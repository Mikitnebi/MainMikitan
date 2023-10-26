using MainMikitan.Domain.Requests.Customer;

namespace MainMikitan.Database.Features.Customer.Interface;

public interface ICustomerInfoRepository
{
    Task<bool> CreateOrUpdate(CreateOrUpdateCustomerInfoRequest customerInfo, int customerId);
    Task<bool> SaveChanges();
}