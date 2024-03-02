using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain.Models.Restaurant;
using Microsoft.EntityFrameworkCore;

namespace MainMikitan.Database.Features.Restaurant.Command {
    public class RestaurantEnvCommandRepository
        (MikDbContext mikDbContext) : IRestaurantEnvCommandRepository
    {

        public async Task<bool> Delete(int restaurantId, CancellationToken cancellationToken = default) {
            
            var test = await mikDbContext.RestaurantEnvironmentInfo.Where(r => r.RestaurantId == restaurantId).ToListAsync(cancellationToken: cancellationToken);
            if (test.Count == 0) return true;
            var result = await mikDbContext.RestaurantEnvironmentInfo
                .Where(r => r.RestaurantId == restaurantId)
                .ExecuteDeleteAsync(cancellationToken: cancellationToken);
            return result > 0;
        }

        public async Task<bool> Add(IEnumerable<int> environmentIds, int restaurantId, CancellationToken cancellationToken = default)
        {
            foreach (var restaurantEnvironmentEntity in environmentIds.Select(environment => new RestaurantEnvEntity {
                         CreatedAt = DateTime.Now,
                         EnvironmentId = environment,
                         RestaurantId = restaurantId,
                         IsActive = true
                     }))
            {
                
                var result = await mikDbContext.RestaurantEnvironmentInfo.AddAsync(restaurantEnvironmentEntity, cancellationToken);
                if (result.State != EntityState.Added) return false;
            }

            return true;
        }
        public async Task<bool> SaveChanges(CancellationToken cancellationToken = default) {
            var result = await mikDbContext.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
    }
}
