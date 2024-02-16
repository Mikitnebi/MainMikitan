using MainMikitan.Domain.Models.Restaurant;

namespace MainMikitan.Database.Features.Restaurant.Interface;

public interface IRestaurantEnvQueryRepository
{ 
    Task<List<RestaurantEnvEntity>> Get(int restaurantId, CancellationToken cancellationToken = default);
}