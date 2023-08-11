using MainMikitan.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Database.Features.Common.Otp.Interfaces
{
    public interface IOtpLogCommandRepository
    {
        Task<int?> Create(OtpLogIntroEntity model);
    }
}
