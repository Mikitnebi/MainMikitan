using MainMikitan.Database.Features.Dish.Interface;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests;
using MainMikitan.Domain.Responses.DishResponses;
using MainMikitan.Domain.Responses.S3Response;
using MainMikitan.ExternalServicesAdapter.S3ServiceAdapter;
using MediatR;

namespace MainMikitan.Application.Features.Dish.Get.Commands;

public class GetAllDishesCommand : IRequest<ResponseModel<List<GetDishInfoResponse>>>
{
    public int RestaurantId { get; }

    public GetAllDishesCommand(int restaurantId)
    {
        RestaurantId = restaurantId;
    }
}

public class GetAllDishesHandler : IRequestHandler<GetAllDishesCommand, ResponseModel<List<GetDishInfoResponse>>>
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
            data.AddRange(images.ImagesData);
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