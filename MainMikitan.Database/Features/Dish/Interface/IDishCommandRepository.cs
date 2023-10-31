using MainMikitan.Domain.Requests;
using MainMikitan.Domain.Responses.DishResponses;

namespace MainMikitan.Database.Features.Dish.Interface;

public interface IDishCommandRepository
{
    public Task AddDishCategory(AddCategoryDishRequest request);
    public Task AddDishInfo(AddDishInfoRequest request);
    public Task AddDish(AddDishRequest request);
    public Task<bool> SaveDishChanges();
    public Task<bool> UpdateDishInfo(UpdateDishInfoRequest request);
    public Task<bool> DeactiveDish(DeactiveDishRequest request);
    public Task<bool> DeleteDish(DeleteDishRequest request);
    public Task<bool> VerifyDish(VerifyDishRequest request);
    public List<GetDishInfoResponse> GetAllDishes(GetAllDishesRequest request);
}