using System.Data.SqlClient;
using Dapper;
using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain.Models.Customer;
using MainMikitan.Domain.Models.Setting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MainMikitan.Database.Features.Customer.Query;

public class CustomerInfoQueryRepository(MikDbContext db) : ICustomerInfoQueryRepository
{
    
    public async Task<CustomerInfoEntity?> Get(int customerId, CancellationToken cancellationToken = default)
    {
        var customerInfoResponse = await db.CustomerInfo.FirstOrDefaultAsync(t => t.CustomerId == customerId,cancellationToken);
        return customerInfoResponse ?? null;
    }
}
