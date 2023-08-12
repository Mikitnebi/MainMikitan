using MainMikitan.Domain.Models.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Domain.Interfaces.Customer
{
    public interface ICustomerCommandRepository
    {
        Task<int?> CreateOrUpdate(CustomerEntity entity);
    }
}
