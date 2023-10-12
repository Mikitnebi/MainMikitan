using MainMikitan.Domain.Models.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Database.Features.Customer.Interface
{
    public interface ICustomerQueryRepository
    {
        Task<CustomerEntity> GetByEmail(string email);
        Task<CustomerEntity> GetByMobileNumber(string mobileNumber);
        Task<CustomerEntity> GetNonVerifiedByEmail(string email);
        Task ContainsId();
    }
}
