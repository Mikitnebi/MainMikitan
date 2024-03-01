using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain.Models.Restaurant;
using MainMikitan.Domain.Requests.RestaurantRequests;
using Microsoft.EntityFrameworkCore;

namespace MainMikitan.Database.Features.Restaurant.Command;

public class RestaurantInfoCommandRepository(MikDbContext mikDbContext) : IRestaurantInfoCommandRepository
{
    public async Task<bool> Create(RestaurantInfoEntity entity, CancellationToken cancellationToken = default)
    {
        entity.CreatedAt = DateTime.Now;
        var createResponse = await mikDbContext.RestaurantInfo.AddAsync(entity, cancellationToken);
        return createResponse.State == EntityState.Added;
    }
    public bool Update(RestaurantInfoEntity entity)
    {
        entity.UpdatedAt = DateTime.Now;
        var updateResponse = mikDbContext.RestaurantInfo.Update(entity);
        return updateResponse.State == EntityState.Modified;
    }

    public async Task<bool> SaveChanges(CancellationToken cancellationToken = default)
    {
        var saveResponse = await mikDbContext.SaveChangesAsync(cancellationToken);
        return saveResponse != 0;
    }
}