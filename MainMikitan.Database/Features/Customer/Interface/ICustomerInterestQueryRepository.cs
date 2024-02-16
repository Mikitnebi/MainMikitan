using MainMikitan.Domain.Models.Customer;
using MainMikitan.Domain.Responses.Customer;

namespace MainMikitan.Database.Features.Customer.Interface;

public interface ICustomerInterestQueryRepository
{
    Task<List<CustomerInterestEntity>> GetByCustomerId(int customerId, CancellationToken cancellationToken = default);
    Task<List<int>> GetInterestIdsByCustomerId(int customerId, CancellationToken cancellationToken = default);
}