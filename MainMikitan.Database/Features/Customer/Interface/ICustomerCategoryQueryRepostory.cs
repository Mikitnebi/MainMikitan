using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Database.Features.Category.Query
{
    public interface ICustomerCategoryQueryRepostory
    {
        public Task AddInterestTagId(List<int> interestIds, int customerId);
    }
}
