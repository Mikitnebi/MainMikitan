using MainMikitan.Domain.Models.Customer;

namespace MainMikitan.Database.Features.Customer.Interface;

public interface ICustomerInterestRepository
{
    Task<bool> Delete(int customerId, CancellationToken cancellationToken = default);
    Task<bool> Add(List<int> interestIds, int customerId, CancellationToken cancellationToken = default);
    Task<List<CustomerInterestEntity>> Get(int customerId, CancellationToken cancellationToken = default);
    Task<bool> SaveChanges(CancellationToken cancellationToken = default);

}