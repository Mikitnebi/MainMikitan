using MainMikitan.Database.Features.Dish.Interface;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests;
using MainMikitan.Domain.Responses.DishResponses;
using MainMikitan.Domain.Responses.S3Response;
using MainMikitan.ExternalServicesAdapter.S3ServiceAdapter;
using MediatR;

namespace MainMikitan.Application.Features.Dish.Get.Commands;

public class GetAllDishesForCustomerCommand : IRequest<ResponseModel<List<GetAllDishesForCustomerResponse>>>
{
    public int RestaurantId { get; }

    public GetAllDishesForCustomerCommand(GetAllDishesForCustomerRequest request)
    {
        RestaurantId = request.RestaurantId;
    }
}

public class GetAllDishesForCustomerHandler : IRequestHandler<GetAllDishesForCustomerCommand, ResponseModel<List<GetAllDishesForCustomerResponse>>>
{
    private readonly IDishCommandRepository _dishCommandRepository;
    private readonly IS3Adapter _s3Adapter;
    
    public GetAllDishesForCustomerHandler(IDishCommandRepository dishCommandRepository, IS3Adapter s3Adapter)
    {
        _dishCommandRepository = dishCommandRepository;
        _s3Adapter = s3Adapter;
    }

    public async Task<ResponseModel<List<GetAllDishesForCustomerResponse>>> Handle(GetAllDishesForCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var dishes = _dishCommandRepository.GetAllDishesForCustomer(request.RestaurantId);
        
        List<GetImageResponse> data = new();
        var dishCategories = dishes.Select(d => d.CategoryId).Distinct();
        foreach (var categoryId in dishCategories)
        {
            var images = await _s3Adapter.GetRestaurantDishCategoryImages(request.RestaurantId, categoryId);
            data.AddRange(images.Result.ImagesData);
        }
        
        foreach (var dish in dishes)
        {
            dish.DishPicture = data.FirstOrDefault(d => d.DishId == dish.DishId)!.Url;
        }

        return new ResponseModel<List<GetAllDishesForCustomerResponse>>
        {
            Result = dishes
        };
    }
}