using MainMikitan.Domain.Models.Customer;

namespace MainMikitan.Database.Features.Customer.Interface
{
    public interface ICustomerQueryRepository
    {
        Task<CustomerEntity?> GetById(int id, CancellationToken cancellationToken = default);
        Task<CustomerEntity?> GetByEmail(string email, CancellationToken cancellationToken = default);
        Task<CustomerEntity?> GetByMobileNumber(string mobileNumber, CancellationToken cancellationToken = default);
        Task<CustomerEntity?> GetNonVerifiedByEmail(string email, CancellationToken cancellationToken = default);
    }
}
