using MainMikitan.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MainMikitan.Domain.Enums;

namespace MainMikitan.Database.Features.Common.Otp.Interfaces
{
    public interface IOtpLogCommandRepository
    {
        Task<bool> Create(OtpLogIntroEntity model, CancellationToken cancellationToken = default);
        Task<bool> Update(int id, int numberOfTrials, int status, CancellationToken cancellationToken = default);
    }
}
