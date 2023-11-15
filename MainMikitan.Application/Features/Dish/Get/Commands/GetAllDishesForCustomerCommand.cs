using MainMikitan.Database.Features.Dish.Interface;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests;
using MainMikitan.Domain.Responses.DishResponses;
using MainMikitan.Domain.Responses.S3Response;
using MainMikitan.Domain.Templates;
using MainMikitan.ExternalServicesAdapter.S3ServiceAdapter;

namespace MainMikitan.Application.Features.Dish.Get.Commands;

public class GetAllDishesForCustomerQuery: IQuery<List<GetAllDishesForCustomerResponse>>
{
    public int RestaurantId { get; }
    public int? CategoryId { get; }

    public GetAllDishesForCustomerQuery(GetAllDishesForCustomerRequest request)
    {
        RestaurantId = request.RestaurantId;
        CategoryId = request.CategoryId;
    }
}

public class GetAllDishesForCustomerQueryHandler : IQueryHandler<GetAllDishesForCustomerQuery, List<GetAllDishesForCustomerResponse>>
{
    private readonly IDishCommandRepository _dishCommandRepository;
    private readonly IS3Adapter _s3Adapter;
    
    public GetAllDishesForCustomerQueryHandler(IDishCommandRepository dishCommandRepository, IS3Adapter s3Adapter)
    {
        _dishCommandRepository = dishCommandRepository;
        _s3Adapter = s3Adapter;
    }

    public async Task<ResponseModel<List<GetAllDishesForCustomerResponse>>> Handle(GetAllDishesForCustomerQuery request,
        CancellationToken cancellationToken)
    {
        var dishes = request.CategoryId is null
            ? _dishCommandRepository.GetAllDishesForCustomer(request.RestaurantId)
            : _dishCommandRepository.GetAllDishesWithCategoryForCustomer(request.RestaurantId, request.CategoryId);
        
        List<GetImageResponse> data = new();
        var dishCategories = dishes.Select(d => d.CategoryId).Distinct();
        foreach (var categoryId in dishCategories)
        {
            var images = await _s3Adapter.GetRestaurantDishCategoryImages(request.RestaurantId, categoryId);
            data.AddRange(images!.ImagesData);
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