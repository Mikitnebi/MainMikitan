using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.Customer.Interface;
using  MainMikitan.Domain.Models.Customer;
using MainMikitan.Domain.Requests.Customer;
using Microsoft.EntityFrameworkCore;

namespace MainMikitan.Database.Features.Customer.Command;

public class CustomerInfoRepository : ICustomerInfoRepository
{
    public readonly MikDbContext _db;

    public CustomerInfoRepository(
        MikDbContext db
        )
    {
        _db = db;
    }

    public async Task<bool> CreateOrUpdate(CreateOrUpdateCustomerInfoRequest customerInfo, int customerId)
    {
        var customerInfoEntity = await _db.CustomerInfo.
            FirstOrDefaultAsync(t => t.CustomerId == customerId);
        if (customerInfoEntity is not null)
        {
            var result = await _db.CustomerInfo.AddAsync(new CustomerInfoEntity
            {
                CustomerId = customerId,
                GenderId = customerInfo.GenderId,
                NationalityId = customerInfo.NationalityId,
                BirthDate = (DateOnly)customerInfo.BirthDate!,
                FullName = customerInfo.FullName,
                CreatedAt = DateTime.Now
            });
            if (result.Entity.CustomerId == 0)
                return false;
        }
        else
        {
            customerInfoEntity!.GenderId = customerInfo.GenderId != 0 ? customerInfo.GenderId : customerInfoEntity.GenderId;
            customerInfoEntity!.NationalityId = customerInfo.NationalityId != 0 ? customerInfo.NationalityId : customerInfoEntity.NationalityId;
            customerInfoEntity!.FullName = customerInfo.FullName != string.Empty ? customerInfo.FullName : customerInfoEntity.FullName;
            customerInfoEntity!.BirthDate = customerInfo.BirthDate is not null
                ? (DateOnly)customerInfo.BirthDate!
                : customerInfoEntity.BirthDate;
            customerInfoEntity.UpdatedAt = DateTime.Now;
            return true;
        }
        
        return true;
    }

    public async Task<CustomerInfoEntity?> Get(int customerId)
    {
        var customerInfoResponse = await _db.CustomerInfo.FirstOrDefaultAsync(t => t.CustomerId == customerId);
        return customerInfoResponse ?? null;
    }

    public async Task<bool> Delete(int customerId)
    {
        var deleteCustomerInfoResponse =
            await _db.CustomerInfo.Where(t => t.CustomerId == customerId).ExecuteDeleteAsync();
        return deleteCustomerInfoResponse > 0;
    }
    public async Task<bool> SaveChanges()
    {
        var result = await _db.SaveChangesAsync();
        return result > 0;
    }
}