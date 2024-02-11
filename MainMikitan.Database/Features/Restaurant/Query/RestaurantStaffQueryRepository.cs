using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain.Models.Restaurant;
using Microsoft.EntityFrameworkCore;

namespace MainMikitan.Database.Features.Restaurant.Query;

public class RestaurantStaffQueryRepository(MikDbContext db) : IRestaurantStaffQueryRepository
{
    public async Task<RestaurantStaffEntity?> GetActive(string hashUserName, string hashPassword, CancellationToken cancellationToken = default)
    {
        return await db.RestaurantStaff.FirstOrDefaultAsync(
            t => t.PasswordHash == hashPassword && t.UserNameHash == hashUserName && t.IsActive == true, cancellationToken);
    }
}