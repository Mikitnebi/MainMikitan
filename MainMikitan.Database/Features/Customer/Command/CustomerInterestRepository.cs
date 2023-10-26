using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain.Models.Customer;
using Microsoft.EntityFrameworkCore;

namespace MainMikitan.Database.Features.Customer.Command;

public class CustomerInterestRepository : ICustomerInterestRepository
{
    public MikDbContext _mikDbContext;
    public CustomerInterestRepository(MikDbContext mikDbContext)
    {
        _mikDbContext = mikDbContext;
    }

    public async Task<bool> Delete(int customerId)
    {
        var result = await _mikDbContext.CustomerInterest.Where(t => t.CustomerId == customerId).ExecuteDeleteAsync();
        return result > 0;
    }
    public async Task<bool> Add(List<int> interestIds, int customerId)
    {
        for (int i = 0; i < interestIds.Count; i++)
        {
            var interestEntity = new CustomerInterestEntity
            {
                CreatedAt = DateTime.Now,
                InterestId = interestIds[i],
                CustomerId = customerId
            };
            var result = await _mikDbContext.CustomerInterest.AddAsync(interestEntity);
            if (result.Entity == null) return false;
        }
        return true;
    }

    public async Task<bool> SaveChanges()
    {
        var result = await _mikDbContext.SaveChangesAsync();
        return result > 0;
    }
}