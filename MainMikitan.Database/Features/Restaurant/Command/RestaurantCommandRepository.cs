using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.Common.Multifunctional.Interface.Repository;
using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain.Models.Restaurant;
using MainMikitan.Domain.Models.Setting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MainMikitan.Database.Features.Restaurant.Command
{
    public class RestaurantCommandRepository(
        MikDbContext db
        )
        : IRestaurantCommandRepository
    {

        public async Task<bool> Create(RestaurantEntity restaurant, CancellationToken cancellationToken = default)
        {
            var createResponse = await db.Restaurant.AddAsync(restaurant, cancellationToken);
            return createResponse.State == EntityState.Added;
        }

        public async Task<bool> SaveChanges(CancellationToken cancellationToken = default)
        {
            return await db.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
