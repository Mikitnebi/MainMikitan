using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.Customer.Interface;
using  MainMikitan.Domain.Models.Customer;
using MainMikitan.Domain.Requests.Customer;
using Microsoft.EntityFrameworkCore;

namespace MainMikitan.Database.Features.Customer.Command;

public class CustomerInfoRepository : ICustomerInfoRepository
{
    public MikDbContext _mikDbContext;

    public CustomerInfoRepository(
        MikDbContext mikDbContext
        )
    {
        _mikDbContext = mikDbContext;
    }

    public async Task<bool> CreateOrUpdate(CreateOrUpdateCustomerInfoRequest customerInfo, int customerId)
    {
        var customerInfoEntity = await _mikDbContext.CustomerInfo.
            FirstOrDefaultAsync(t => t.CustomerId == customerId);
        if (customerInfoEntity is not null)
        {
            var result = await _mikDbContext.CustomerInfo.AddAsync(new CustomerInfoEntity
            {
                CustomerId = customerId,
                GenderId = customerInfo.GenderId,
                NationalityId = customerInfo.NationalityId,
                BirthDate = (DateOnly)customerInfo.BirthDate!,
                FirstName = customerInfo.FirstName,
                LastName = customerInfo.LastName,
                CreatedAt = DateTime.Now
            });
            if (result.Entity.CustomerId == 0)
                return false;
        }
        else
        {
            customerInfoEntity!.GenderId = customerInfo.GenderId != 0 ? customerInfo.GenderId : customerInfoEntity.GenderId;
            customerInfoEntity!.NationalityId = customerInfo.NationalityId != 0 ? customerInfo.NationalityId : customerInfoEntity.NationalityId;
            customerInfoEntity!.FirstName = customerInfo.FirstName != String.Empty ? customerInfo.FirstName : customerInfoEntity.FirstName;
            customerInfoEntity!.LastName = customerInfo.LastName != String.Empty ? customerInfo.LastName : customerInfoEntity.LastName;
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
        var customerInfoResponse = await _mikDbContext.CustomerInfo.FirstOrDefaultAsync(t => t.CustomerId == customerId);
        return customerInfoResponse ?? null;
    }
    public async Task<bool> SaveChanges()
    {
        var result = await _mikDbContext.SaveChangesAsync();
        return result > 0;
    }
}