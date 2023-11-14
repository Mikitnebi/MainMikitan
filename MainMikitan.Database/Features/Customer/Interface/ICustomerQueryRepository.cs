using MainMikitan.Domain.Models.Customer;

namespace MainMikitan.Database.Features.Customer.Interface
{
    public interface ICustomerQueryRepository
    {
        Task<CustomerEntity> GetById(int id);
        Task<CustomerEntity> GetByEmail(string email);
        Task<CustomerEntity> GetByMobileNumber(string mobileNumber);
        Task<CustomerEntity> GetNonVerifiedByEmail(string email);
    }
}
