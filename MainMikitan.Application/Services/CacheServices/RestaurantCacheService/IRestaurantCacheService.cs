using MainMikitan.Domain.Models.Restaurant;

namespace MainMikitan.Application.Services.CacheServices.RestaurantCacheService;

public interface IRestaurantCacheService
{
    public Task<RestaurantSubscriptionInfoEntity?> GetRestaurantSubscriptionTypes(int restaurantId,
        CancellationToken cancellationToken = default);
}