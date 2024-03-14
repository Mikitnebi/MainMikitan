using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain.Models.Restaurant.TableManagement;

namespace MainMikitan.Database.Features.Restaurant.Query;

public class RestaurantQueryRepository(MikDbContext dbContext) : IRestaurantRepository
{
    
}