using MainMikitan.Application.Services.AutoMapper;
using MainMikitan.Application.Services.Permission;
using MainMikitan.Database.Features.Dish.Interface;
using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Responses.RestaurantResponses;
using MainMikitan.Domain.Templates;
using MainMikitan.ExternalServicesAdapter.S3ServiceAdapter;
using Microsoft.EntityFrameworkCore;

namespace MainMikitan.Application.Features.Restaurant.Get;

public class GetMenuCommand(int restaurantId, int userId, string userRole, IEnumerable<int> permissionIds) : IQuery<List<GetMenuResponse>>
{
    public int UserId { get; } = userId;
    public int RestaurantId { get; } = restaurantId;
    public IEnumerable<int> PermissionIds { get; } = permissionIds;
    public string UserRole { get; } = userRole;
}

public class GetMenuHandler(IGetMenuRepository getMenuRepository, 
    IMapperConfig mapperConfig, 
    IS3Adapter s3Adapter, 
    IDishCommandRepository dishCommandRepository,
    IPermissionService permissionService) : ResponseMaker<List<GetMenuResponse>>, IQueryHandler<GetMenuCommand, List<GetMenuResponse>>
{
    public async Task<ResponseModel<List<GetMenuResponse>>> Handle(GetMenuCommand request,
        CancellationToken cancellationToken)
    {
        if (!await permissionService.Check(request.UserId, request.PermissionIds, request.UserRole,
                cancellationToken, request.RestaurantId, 1))
            return Fail(ErrorResponseType.Staff.StaffForbiddenPermission);
        
        var response = new ResponseModel<List<GetMenuResponse>>();
        
        var menu = getMenuRepository.GetRestaurantMenu(request.RestaurantId).AsNoTracking().ToList();
        
        foreach (var dish in menu)
        {
            var responseDish = new GetMenuResponse();
            var dishIdForPicture = dish.HasDifferentPicture ? dish.Id : dish.ParentDishId;
            var newDish = await dishCommandRepository.GetDish(request.RestaurantId, (int)dishIdForPicture!);
            
            var dishPicturePath = await s3Adapter.GetRestaurantDishCategoryImages(request.RestaurantId, newDish!.CategoryId);
            responseDish.DishPicture = dishPicturePath.Result!.ImagesData.FirstOrDefault()!.Url;
            
            response.Result?.Add(mapperConfig.Map(dish, responseDish)); 
        }
        
        return response;
    }
}