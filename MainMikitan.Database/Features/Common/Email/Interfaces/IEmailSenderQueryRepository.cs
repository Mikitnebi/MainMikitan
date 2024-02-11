using MainMikitan.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Database.Features.Common.Email.Interfaces
{
    public interface IEmailSenderQueryRepository
    {
        Task<EmailDictionaryEntity?> GetEmailById(int emailTypeId, CancellationToken cancellationToken = default);
    }
}
