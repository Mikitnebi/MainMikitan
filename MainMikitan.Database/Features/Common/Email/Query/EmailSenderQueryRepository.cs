using Dapper;
using MainMikitan.Database.Features.Common.Email.Interfaces;
using MainMikitan.Domain.Models.Common;
using MainMikitan.Domain.Models.Setting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainMikitan.Database.DbContext;
using Microsoft.EntityFrameworkCore;
using static MainMikitan.Domain.Enums;

namespace MainMikitan.Database.Features.Common.Query
{
    public class EmailSenderQueryRepository(MikDbContext db)
        : IEmailSenderQueryRepository
    {
        public async Task<EmailDictionaryEntity?> GetEmailById(int emailTypeId, CancellationToken cancellationToken = default)
        {
            return await db.EmailDictionary.FirstOrDefaultAsync(t => t.Id == emailTypeId, cancellationToken);
        }
    }
}
