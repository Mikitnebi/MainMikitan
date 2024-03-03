using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.Rating.Interface;
using MainMikitan.Domain.Models.Rating;
using Microsoft.EntityFrameworkCore;

namespace MainMikitan.Database.Features.Rating.Query;

public class RestaurantRatingRepository(MikDbContext db) : IRestaurantRatingRepository
{
    public async Task<List<RestaurantRatingEntity>> GetRestaurantRatings(int restaurantId, CancellationToken cancellationToken)
    {
        return await db.RestaurantRating.Where(cr => cr.RestaurantId == restaurantId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
    
    public async Task<List<RestaurantRatingEntity>> GetAllActiveCustomersRatings(CancellationToken cancellationToken)
    {
        return await db.RestaurantRating.Where(cr => cr.CreatedAt > DateTime.Now.AddYears(-1))
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}