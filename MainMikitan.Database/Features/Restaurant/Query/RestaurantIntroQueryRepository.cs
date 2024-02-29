using Dapper;
using MainMikitan.Domain.Interfaces.Restaurant;
using MainMikitan.Domain.Models.Restaurant;
using MainMikitan.Domain.Models.Setting;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using MainMikitan.Database.DbContext;
using Microsoft.EntityFrameworkCore;

namespace MainMikitan.Database.Features.Restaurant.Query {
    public class RestaurantIntroQueryRepository(MikDbContext db)
        : IRestaurantIntroQueryRepository
    {

        public async Task<RestaurantIntroEntity?> GetNonVerifiedByEmail(string email, CancellationToken cancellationToken = default) {
            return await db.RestaurantIntro.Where(t =>
                t.EmailAddress == email && t.EmailConfirmation == false)
                .OrderByDescending(t => t.Id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<RestaurantIntroEntity?> GetVerifiedByEmail(string email, CancellationToken cancellationToken = default)
        {
            return await db.RestaurantIntro.FirstOrDefaultAsync(t =>
                t.EmailAddress == email && t.EmailConfirmation == true, cancellationToken: cancellationToken);
        }
    }
}
