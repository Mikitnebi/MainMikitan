using MainMikitan.Database.Features.Dish.Interface;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Responses.DishResponses;
using MainMikitan.Domain.Responses.S3Response;
using MainMikitan.Domain.Templates;
using MainMikitan.ExternalServicesAdapter.S3ServiceAdapter;

namespace MainMikitan.Application.Features.Dish.Get.Commands;

public class GetAllDishesCommand : IQuery<List<GetDishInfoResponse>>
{
    public int RestaurantId { get; }

    public GetAllDishesCommand(int restaurantId)
    {
        RestaurantId = restaurantId;
    }
}

public class GetAllDishesHandler : IQueryHandler<GetAllDishesCommand, List<GetDishInfoResponse>>
{
    private readonly IDishCommandRepository _dishCommandRepository;
    private readonly IS3Adapter _s3Adapter;
    
    public GetAllDishesHandler(IDishCommandRepository dishCommandRepository, IS3Adapter s3Adapter)
    {
        _dishCommandRepository = dishCommandRepository;
        _s3Adapter = s3Adapter;
    }

    public async Task<ResponseModel<List<GetDishInfoResponse>>> Handle(GetAllDishesCommand request,
        CancellationToken cancellationToken)
    {
        var dishes = _dishCommandRepository.GetAllDishes(request.RestaurantId);
        
        List<GetImageResponse> data = new();
        var dishCategories = dishes.Select(d => d.CategoryId).Distinct();
        foreach (var categoryId in dishCategories)
        {
            var images = await _s3Adapter.GetRestaurantDishCategoryImages(request.RestaurantId, categoryId);
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