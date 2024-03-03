using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.Rating.Interface;
using MainMikitan.Domain.Models.Rating;
using Microsoft.EntityFrameworkCore;

namespace MainMikitan.Database.Features.Rating.Query;

public class CustomerRatingRepository(MikDbContext db) :ICustomerRatingRepository
{
    public async Task<List<CustomerRatingEntity>> GetCustomerRatings(int customerId, CancellationToken cancellationToken)
    {
        return await db.CustomerRating.Where(cr => cr.UserId == customerId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
    
    public async Task<List<CustomerRatingEntity>> GetAllActiveCustomersRatings(CancellationToken cancellationToken)
    {
        return await db.CustomerRating.Where(cr => cr.CreatedAt > DateTime.Now.AddYears(-1))
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}