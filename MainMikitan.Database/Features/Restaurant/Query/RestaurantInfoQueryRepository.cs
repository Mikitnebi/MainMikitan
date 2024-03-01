using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain.Models.Restaurant;
using Microsoft.EntityFrameworkCore;

namespace MainMikitan.Database.Features.Restaurant.Query;

public class RestaurantInfoQueryRepository(MikDbContext db) : IRestaurantInfoQueryRepository
{

    public async Task<List<RestaurantInfoEntity>> GetRestaurantsByCustom(
        int regionId, int? businessTypeId = null, bool? hasCoupe = null, bool? hasTerrace = null,
        int? isActiveInSomeHour = null, int? isActiveKitchenInSomeHour = null, int? isActiveMusicInSomeHour = null, 
        int? priceTypeId = null, int? rate = null, CancellationToken cancellationToken = default)
    {
        var query = db.RestaurantInfo.Where(t => t.RegionId == regionId);
        if (hasCoupe is not null) query = query.Where(t => t.HasCoupe == hasCoupe);
        if (hasTerrace is not null) query = query.Where(t => t.HasCoupe == hasTerrace);
        if (businessTypeId is not null) query = query.Where(t => t.BusinessTypeId == businessTypeId);
        if (isActiveInSomeHour is not null)
        {
            var activeTime = TimeOnly.FromDateTime(DateTime.Now.AddHours((int)isActiveInSomeHour));
            query = query.Where(t =>
                t.HallStartTime < activeTime && activeTime < t.HallEndTime);
        }
        if (isActiveKitchenInSomeHour is not null)
        {
            var activeKitchenTime = TimeOnly.FromDateTime(DateTime.Now.AddHours((int)isActiveKitchenInSomeHour));
            query = query.Where(t =>
                t.KitchenStartTime < activeKitchenTime && activeKitchenTime < t.KitchenEndTime);
        }
        if (isActiveMusicInSomeHour is not null)
        {
            var activeMusicTime = TimeOnly.FromDateTime(DateTime.Now.AddHours((int)isActiveMusicInSomeHour));
            query = query.Where(t =>
                t.MusicStartTime < activeMusicTime && activeMusicTime < t.MusicEndTime
            );
        }

        if (priceTypeId is not null) query = query.Where(t => t.PriceTypeId == priceTypeId);
        if (rate is not null) query = query.Where(t => t.Rate > rate);
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<RestaurantInfoEntity> GetByRestaurantId(int restaurantId, CancellationToken cancellationToken = default)
    {
        return await db.RestaurantInfo.Where(t => t.RestaurantId == restaurantId).FirstAsync(cancellationToken);
    }
}