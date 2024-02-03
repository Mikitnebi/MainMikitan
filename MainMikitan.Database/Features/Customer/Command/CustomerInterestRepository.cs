using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain.Models.Customer;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;

namespace MainMikitan.Database.Features.Customer.Command;

public class CustomerInterestRepository(MikDbContext mikDbContext) : ICustomerInterestRepository
{
    public async Task<bool> Delete(int customerId, CancellationToken cancellationToken = default)
    {
        var result = await mikDbContext.CustomerInterest.Where(t => t.CustomerId == customerId).ExecuteDeleteAsync(cancellationToken: cancellationToken);
        return result > 0;
    }
    public async Task<bool> Add(List<int> interestIds, int customerId, CancellationToken cancellationToken = default)
    {
        for (var i = 0; i < interestIds.Count; i++)
        {
            var interestEntity = new CustomerInterestEntity
            {
                CreatedAt = DateTime.Now,
                InterestId = interestIds[i],
                CustomerId = customerId
            };
            var result = await mikDbContext.CustomerInterest.AddAsync(interestEntity, cancellationToken);
            if (result.State != EntityState.Added) return false;
        }
        return true;
    }

    public async Task<List<CustomerInterestEntity>> Get(int customerId, CancellationToken cancellationToken = default)
    {
        return await mikDbContext.CustomerInterest.Where(t => t.CustomerId == customerId).ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<bool> SaveChanges(CancellationToken cancellationToken = default)
    {
        var result = await mikDbContext.SaveChangesAsync(cancellationToken);
        return result > 0;
    }
}