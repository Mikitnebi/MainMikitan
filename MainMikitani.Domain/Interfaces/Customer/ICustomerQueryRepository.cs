using MainMikitan.Domain.Models.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Domain.Interfaces.Customer
{
    public interface ICustomerQueryRepository
    {
        Task<CustomerEntity> GetByEmail(string email);
        Task<CustomerEntity> GetNonVerifiedByEmail(string email);
    }
}
