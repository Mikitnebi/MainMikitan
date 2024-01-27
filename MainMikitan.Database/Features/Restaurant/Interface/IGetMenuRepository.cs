using MainMikitan.Domain.Models.Menu;

namespace MainMikitan.Database.Features.Restaurant.Interface;

public interface IGetMenuRepository
{
    public IQueryable<DishEntity> GetRestaurantMenu(int restaurantId);
}