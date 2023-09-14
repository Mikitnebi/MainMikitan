using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Database.Features.Common.Email.Interfaces
{
    public interface IEmailLogCommandRepository
    {
        Task<int?> Create(string email, int typeId, int userId, int userTypeId, string data);
    }
}
