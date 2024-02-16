using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain.Models.Restaurant;
using Microsoft.EntityFrameworkCore;

namespace MainMikitan.Database.Features.Restaurant.Command;

public class RestaurantEnvQueryRepository(MikDbContext mikDbContext) : IRestaurantEnvQueryRepository
{
    public async Task<List<RestaurantEnvEntity>> Get(int restaurantId, CancellationToken cancellationToken = default) {
        return await mikDbContext.RestaurantEnvironmentInfo
            .Where(r => r.RestaurantId == restaurantId).AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
    }
}