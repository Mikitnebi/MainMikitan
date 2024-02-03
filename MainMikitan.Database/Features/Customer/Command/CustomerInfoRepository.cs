using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.Customer.Interface;
using  MainMikitan.Domain.Models.Customer;
using MainMikitan.Domain.Requests.Customer;
using Microsoft.EntityFrameworkCore;

namespace MainMikitan.Database.Features.Customer.Command;

public class CustomerInfoRepository(MikDbContext db) : ICustomerInfoRepository
{

    public async Task<bool> CreateOrUpdate(CreateOrUpdateCustomerInfoRequest customerInfo, int customerId, CancellationToken cancellationToken = default)
    {
        var customerInfoEntity = await db.CustomerInfo.
            FirstOrDefaultAsync(t => t.CustomerId == customerId, cancellationToken);
        if (customerInfoEntity is null)
        {
            var result = await db.CustomerInfo.AddAsync(new CustomerInfoEntity
            {
                CustomerId = customerId,
                GenderId = customerInfo.GenderId,
                NationalityId = customerInfo.NationalityId,
                BirthDate = (DateOnly)customerInfo.BirthDate!,
                CreatedAt = DateTime.Now
            }, cancellationToken);
            return await SaveChanges(cancellationToken);
        }
        else
        {
            customerInfoEntity!.GenderId = customerInfo.GenderId != 0 ? customerInfo.GenderId : customerInfoEntity.GenderId;
            customerInfoEntity!.NationalityId = customerInfo.NationalityId != 0 ? customerInfo.NationalityId : customerInfoEntity.NationalityId;
            customerInfoEntity!.BirthDate = customerInfo.BirthDate is not null
                ? (DateOnly)customerInfo.BirthDate!
                : customerInfoEntity.BirthDate;
            customerInfoEntity.UpdatedAt = DateTime.Now;
            return true;
        }
        
        return true;
    }

    public async Task<CustomerInfoEntity?> Get(int customerId, CancellationToken cancellationToken = default)
    {
        var customerInfoResponse = await db.CustomerInfo.FirstOrDefaultAsync(t => t.CustomerId == customerId,cancellationToken);
        return customerInfoResponse ?? null;
    }

    public async Task<bool> Delete(int customerId, CancellationToken cancellationToken = default)
    {
        var deleteCustomerInfoResponse =
            await db.CustomerInfo.Where(t => t.CustomerId == customerId).ExecuteDeleteAsync(cancellationToken);
        return deleteCustomerInfoResponse > 0;
    }
    public async Task<bool> SaveChanges(CancellationToken cancellationToken = default)
    {
        var result = await db.SaveChangesAsync(cancellationToken);
        return result > 0;
    }
}