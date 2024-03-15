using MainMikitan.Domain.Models.Restaurant;

namespace MainMikitan.Database.Features.Restaurant.Interface;

public interface IRestaurantSubscriptionRepository
{
    public Task<RestaurantSubscriptionsEntity?> GetSubscription(int restaurantId, CancellationToken cancellationToken);
}