using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Database.Features.Customer.Interface
{
    public interface ICustomerCategoryInfoCommandRepository
    {
        Task<bool> CreateOrUpdate(List<int> ids, int customerId);
        Task Delete(int customerId);
        Task<bool> Insert(List<int> ids, int customerId);
    }
}
