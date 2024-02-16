using MainMikitan.Domain.Models.Customer;
namespace MainMikitan.Database.Features.Customer.Interface;

public interface ICustomerInfoQueryRepository
{
    Task<CustomerInfoEntity?> Get(int customerId, CancellationToken cancellationToken = default);
}