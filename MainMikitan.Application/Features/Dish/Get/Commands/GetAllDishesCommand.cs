using MainMikitan.Application.Services.Permission;
using MainMikitan.Database.Features.Dish.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Responses.DishResponses;
using MainMikitan.Domain.Responses.S3Response;
using MainMikitan.Domain.Templates;
using MainMikitan.ExternalServicesAdapter.S3ServiceAdapter;

namespace MainMikitan.Application.Features.Dish.Get.Commands;

public class GetAllDishesCommand(int restaurantId, int userId, string userRole, IEnumerable<int> permissionIds) : IQuery<List<GetDishInfoResponse>>
{
    public int UserId { get; } = userId;
    public int RestaurantId { get; } = restaurantId;
    public IEnumerable<int> PermissionIds { get; } = permissionIds;
    public string UserRole { get; } = userRole;
}

public class GetAllDishesHandler(IDishCommandRepository dishCommandRepository, IPermissionService permissionService, IS3Adapter s3Adapter) : ResponseMaker<List<GetDishInfoResponse>>, IQueryHandler<GetAllDishesCommand, List<GetDishInfoResponse>>
{
    public async Task<ResponseModel<List<GetDishInfoResponse>>> Handle(GetAllDishesCommand request,
        CancellationToken cancellationToken)
    {
        if (!await permissionService.Check(request.UserId, request.PermissionIds, request.UserRole,
                cancellationToken, request.RestaurantId, 1))
            return Fail(ErrorResponseType.Staff.StaffForbiddenPermission);
        
        var dishes = dishCommandRepository.GetAllDishes(request.RestaurantId);
        
        List<GetImageResponse> data = [];
        var dishCategories = dishes.Select(d => d.CategoryId).Distinct();
        foreach (var categoryId in dishCategories)
        {
            var images = await s3Adapter.GetRestaurantDishCategoryImages(request.RestaurantId, categoryId);
            data.AddRange(images.Result!.ImagesData);
        }
        
        foreach (var dish in dishes)
        {
            dish.DishPicture = data.FirstOrDefault(d => d.DishId == dish.DishId)!.Url;
        }

        return new ResponseModel<List<GetDishInfoResponse>>
        {
            Result = dishes
        };;
    }
}