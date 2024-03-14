using MainMikitan.Database.Features.Dish.Interface;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests;
using MainMikitan.Domain.Responses.DishResponses;
using MainMikitan.Domain.Responses.S3Response;
using MainMikitan.Domain.Templates;
using MainMikitan.ExternalServicesAdapter.S3ServiceAdapter;

namespace MainMikitan.Application.Features.Dish.Get.Commands;

public class GetAllDishesForCustomerQuery(GetAllDishesForCustomerRequest request) : IQuery<List<GetAllDishesForCustomerResponse>>
{
    public int RestaurantId { get; } = request.RestaurantId;
    public int? CategoryId { get; } = request.CategoryId;
}

public class GetAllDishesForCustomerQueryHandler(IDishCommandRepository dishCommandRepository, IS3Adapter s3Adapter) : ResponseMaker<List<GetAllDishesForCustomerResponse>>, IQueryHandler<GetAllDishesForCustomerQuery, List<GetAllDishesForCustomerResponse>>
{

    public async Task<ResponseModel<List<GetAllDishesForCustomerResponse>>> Handle(GetAllDishesForCustomerQuery request,
        CancellationToken cancellationToken)
    {
        var dishes = request.CategoryId is null
            ? dishCommandRepository.GetAllDishesForCustomer(request.RestaurantId)
            : dishCommandRepository.GetAllDishesWithCategoryForCustomer(request.RestaurantId, request.CategoryId);
        
        List<GetImageResponse> data = [];
        var dishCategories = dishes.Select(d => d.CategoryId).Distinct();
        foreach (var categoryId in dishCategories)
        {
            var images = await s3Adapter.GetRestaurantDishCategoryImages(request.RestaurantId, categoryId);
            data.AddRange(images!.Result!.ImagesData);
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