using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.Rating.Interface;
using MainMikitan.Domain.Models.Rating;

namespace MainMikitan.Database.Features.Rating.Command;

public class CustomerRatingCommandRepository(MikDbContext db) : ICustomerRatingCommandRepository
{
    public async Task SaveRating(CustomerRatingEntity ratingEntity)
    {
        await db.CustomerRating.AddAsync(ratingEntity);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await db.SaveChangesAsync() > 0;
    }
}