using Dapper;
using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain.Models.Customer;
using MainMikitan.Domain.Models.Setting;
using MainMikitan.Domain.Responses.Customer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MainMikitan.Database.Features.Customer.Query;

public class CustomerInterestQueryRepository(MikDbContext db) : ICustomerInterestQueryRepository
{
    public async Task<List<CustomerInterestEntity>> GetByCustomerId(int customerId, CancellationToken cancellationToken = default)
    {
        return await db.CustomerInterest.Where(t => t.CustomerId == customerId).ToListAsync(cancellationToken: cancellationToken);
    }
    
    public async Task<List<int>> GetInterestIdsByCustomerId(int customerId, CancellationToken cancellationToken = default)
    {
        return await db.CustomerInterest
            .Where(t => t.CustomerId == customerId)
            .Select(t => t.InterestId)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}