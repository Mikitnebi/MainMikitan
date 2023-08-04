using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Domain.Interfaces.Common
{
    public interface IEmailLogCommandRepository
    {
        Task<int> Create(int EmailTypeId, int userId, int userTypeId);
    }
}
