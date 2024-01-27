using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain.Models.Menu;
using Microsoft.EntityFrameworkCore;

namespace MainMikitan.Database.Features.Restaurant.Query;

public class GetMenuRepository(MikDbContext db) : IGetMenuRepository
{
    private readonly MikDbContext _db = db;

    public IQueryable<DishEntity> GetRestaurantMenu(int restaurantId)
    {
        var result = _db.Dish.Where(r => r.RestaurantId == restaurantId).AsNoTracking();
        
        return result;
    }
}