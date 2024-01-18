using MainMikitan.Domain.Requests;
using MainMikitan.Domain.Responses.DishResponses;

namespace MainMikitan.Database.Features.Dish.Interface;

public interface IDishCommandRepository
{
    public Task AddDishCategory(AddCategoryDishRequest request);
    public Task<int> AddDishInfo(AddDishInfoRequest request);
    public Task<int> AddDish(AddDishRequest request);
    public Task<bool> SaveDishChanges();
    public Task<bool> UpdateDishInfo(UpdateDishInfoRequest request, int userId);
    public Task<bool> UpdateDishStatus(UpdateDishStatusRequest request, int userId);
    public Task<bool> DeleteDish(DeleteDishRequest request);
    public Task<bool> VerifyDish(VerifyDishRequest request);
    public List<GetDishInfoResponse> GetAllDishes(int restaurantId);
    public List<GetAllDishesForCustomerResponse> GetAllDishesForCustomer(int restaurantId);
    public List<GetAllDishesForCustomerResponse> GetAllDishesWithCategoryForCustomer(int restaurantId, int? categoryId);
}