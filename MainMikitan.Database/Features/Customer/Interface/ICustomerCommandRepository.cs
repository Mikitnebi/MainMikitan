using MainMikitan.Domain.Models.Customer;
using static MainMikitan.Domain.Enums;

namespace MainMikitan.Database.Features.Customer.Interface
{
    public interface ICustomerCommandRepository
    {
        Task<bool> CreateOrUpdate(CustomerEntity entity);
        Task<bool> UpdateStatus(string email, bool emailConfirmation, CustomerStatusId status);
        Task<bool> Delete(int userId);
    }
}
