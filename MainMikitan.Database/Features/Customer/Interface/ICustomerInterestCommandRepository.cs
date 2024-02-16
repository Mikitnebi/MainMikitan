using MainMikitan.Domain.Models.Customer;

namespace MainMikitan.Database.Features.Customer.Interface;

public interface ICustomerInterestCommandRepository
{
    Task<bool> Delete(int customerId, CancellationToken cancellationToken = default);
    Task<bool> Add(List<int> interestIds, int customerId, CancellationToken cancellationToken = default);
    Task<bool> SaveChanges(CancellationToken cancellationToken = default);

}