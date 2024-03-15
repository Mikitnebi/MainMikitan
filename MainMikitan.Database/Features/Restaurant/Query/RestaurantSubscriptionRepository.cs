using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain.Models.Restaurant;
using Microsoft.EntityFrameworkCore;

namespace MainMikitan.Database.Features.Restaurant.Query;

public class RestaurantSubscriptionRepository(MikDbContext mikDbContext) : IRestaurantSubscriptionRepository
{
    public async Task<RestaurantSubscriptionsEntity?> GetSubscription(int restaurantId, CancellationToken cancellationToken)
    {
        return await mikDbContext.RestaurantSubscriptions.FirstOrDefaultAsync(rs => rs.RestaurantId == restaurantId, cancellationToken: cancellationToken);
    }
}