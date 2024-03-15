using AutoMapper;
using MainMikitan.Cache.Cache;
using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain.Cache;
using MainMikitan.Domain.Models.Restaurant;

namespace MainMikitan.Application.Services.CacheServices.RestaurantCacheService;

public class RestaurantCacheService(IMemCacheManager memCacheManager,
    IRestaurantSubscriptionRepository restaurantSubscriptionRepository,
    IMapper mapper
    ) : IRestaurantCacheService
{
    public async Task<RestaurantSubscriptionInfoEntity?> GetRestaurantSubscriptionTypes(int restaurantId, CancellationToken cancellationToken = default)
    {
        var cacheKey = CacheKeys.RestaurantSubscription(restaurantId);
        var cacheValue = memCacheManager.Get<RestaurantSubscriptionInfoEntity>(cacheKey);
        if (cacheValue is not null) return cacheValue;

        var subscriptionInfo = await restaurantSubscriptionRepository.GetSubscription(restaurantId, cancellationToken);
        if (subscriptionInfo is null) return null;

        var result = mapper.Map<RestaurantSubscriptionInfoEntity>(subscriptionInfo);
        result.PermissionId = subscriptionInfo.Id;
        result.CacheAddDate = DateTime.Now;
        memCacheManager.Set(cacheKey, result);
        return result;
    }
}