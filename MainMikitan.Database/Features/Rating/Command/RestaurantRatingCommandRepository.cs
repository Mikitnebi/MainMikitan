using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.Rating.Interface;
using MainMikitan.Domain.Models.Rating;

namespace MainMikitan.Database.Features.Rating.Command;

public class RestaurantRatingCommandRepository(MikDbContext db) : IRestaurantRatingCommandRepository
{
    public async Task SaveRating(RestaurantRatingEntity ratingEntity)
    {
        await db.RestaurantRating.AddAsync(ratingEntity);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await db.SaveChangesAsync() > 0;
    }
}