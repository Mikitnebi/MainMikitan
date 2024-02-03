using MainMikitan.Domain.Models.Customer;
using static MainMikitan.Domain.Enums;

namespace MainMikitan.Database.Features.Customer.Interface
{
    public interface ICustomerCommandRepository
    {
        Task<bool> CreateOrUpdate(CustomerEntity entity, CancellationToken cancellationToken = default);
        Task<bool> UpdateStatus(string email, bool emailConfirmation, CustomerStatusId status, CancellationToken cancellationToken = default);
        Task<bool> Delete(int userId, CancellationToken cancellationToken = default);
        bool UpdateCustomer(CustomerEntity customer);
        Task<bool> SaveChanges(CancellationToken cancellationToken = default);
    }
}
