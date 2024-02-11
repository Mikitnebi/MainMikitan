using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain.Models.Restaurant;
using Microsoft.EntityFrameworkCore;

namespace MainMikitan.Database.Features.Restaurant.Command;

public class RestaurantStaffCommandRepository(MikDbContext db) : IRestaurantStaffCommandRepository
{

    public async Task<bool> Add(RestaurantStaffEntity entity, CancellationToken cancellationToken = default)
    {
        var addResponse = await db.RestaurantStaff.AddAsync(entity, cancellationToken);
        return addResponse.State == EntityState.Added;
    }

    public async Task<bool> SaveChanges(CancellationToken cancellationToken = default)
    {
        var saveResponse = await db.SaveChangesAsync(cancellationToken);
        return saveResponse > 0;
    }
}