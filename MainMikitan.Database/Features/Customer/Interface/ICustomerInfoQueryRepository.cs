using MainMikitan.Database.Features.Customer.Query;
using MainMikitan.Domain.Models.Customer;

namespace MainMikitan.Database.Features.Customer.Interface;

public interface ICustomerInfoQueryRepository
{
    public Task<CustomerInfoEntity?> GetVerifiedFromCustomerInfoById(int customerId);
}