using MainMikitan.Domain.Models.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MainMikitan.Domain.Enums;

namespace MainMikitan.Domain.Interfaces.Customer
{
    public interface ICustomerCommandRepository
    {
        Task<int?> CreateOrUpdate(CustomerEntity entity);
        Task<int?> UpdateStatus(string email, bool emailConfrmation, CustomerStatusId status);
    }
}
