using MainMikitan.Domain.Requests;

namespace MainMikitan.Database.Features.Dish.Interface;

public interface IDishCommandRepository
{
    public Task AddDishCategory(AddCategoryDishRequest request);
    public Task AddDishInfo(AddDishInfoRequest request);
    public Task AddDish(AddDishRequest request);
    public Task<bool> SaveDishChanges();
}