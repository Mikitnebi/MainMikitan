using MainMikitan.Domain.Models.Customer;
using MainMikitan.Domain.Requests.Customer;

namespace MainMikitan.Database.Features.Customer.Interface;

public interface ICustomerInfoCommandRepository
{
    Task<bool> CreateOrUpdate(CreateOrUpdateCustomerInfoRequest customerInfo, int customerId, CancellationToken cancellationToken = default);
    Task<bool> Delete(int customerId, CancellationToken cancellationToken = default);
    Task<bool> SaveChanges(CancellationToken cancellationToken = default);
}