using MainMikitan.Application.Services.AutoMapper;
using MainMikitan.Database.Features.Dish.Interface;
using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Responses.RestaurantResponses;
using MainMikitan.Domain.Templates;
using MainMikitan.ExternalServicesAdapter.S3ServiceAdapter;
using Microsoft.EntityFrameworkCore;

namespace MainMikitan.Application.Features.Restaurant.Get;

public class GetMenuCommand : IQuery<List<GetMenuResponse>>
{
    public int RestaurantId { get; }

    public GetMenuCommand(int restaurantId)
    {
        RestaurantId = restaurantId;
    }
}

public class GetMenuHandler : IQueryHandler<GetMenuCommand, List<GetMenuResponse>>
{
    private readonly IGetMenuRepository _getMenuRepository;
    private readonly IMapperConfig _mapperConfig; 
    private readonly IS3Adapter _s3Adapter;
    private readonly IDishCommandRepository _dishCommandRepository;

    public GetMenuHandler(IGetMenuRepository getMenuRepository, 
        IMapperConfig mapperConfig, 
        IS3Adapter s3Adapter, 
        IDishCommandRepository dishCommandRepository)
    {
        _getMenuRepository = getMenuRepository;
        _mapperConfig = mapperConfig;
        _s3Adapter = s3Adapter;
        _dishCommandRepository = dishCommandRepository;
    }

    public async Task<ResponseModel<List<GetMenuResponse>>> Handle(GetMenuCommand request,
        CancellationToken cancellationToken)
    {
        var response = new ResponseModel<List<GetMenuResponse>>();
        
        var menu = _getMenuRepository.GetRestaurantMenu(request.RestaurantId).AsNoTracking().ToList();
        
        foreach (var dish in menu)
        {
            var responseDish = new GetMenuResponse();
            var dishIdForPicture = dish.HasDifferentPicture ? dish.Id : dish.ParentDishId;
            var newDish = await _dishCommandRepository.GetDish(request.RestaurantId, (int)dishIdForPicture!);
            
            var dishPicturePath = await _s3Adapter.GetRestaurantDishCategoryImages(request.RestaurantId, newDish!.CategoryId);
            responseDish.DishPicture = dishPicturePath.Result.ImagesData.FirstOrDefault().Url;
            
            response.Result?.Add(_mapperConfig.Map(dish, responseDish)); 
        }
        
        return response;
    }
}