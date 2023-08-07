using Dapper;
using MainMikitan.Domain.Interfaces.Common;
using MainMikitan.Domain.Models.Common;
using MainMikitan.Domain.Models.Setting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MainMikitan.Domain.Enums;

namespace MainMikitan.Database.Features.Common.Query
{
    public class EmailSenderQueryRepository : IEmailSenderQueryRepository
    {
        public readonly ConnectionStringsOptions _connectionStrings;
        public EmailSenderQueryRepository(
            IOptions<ConnectionStringsOptions> connectionStrings
            )
        {
            _connectionStrings = connectionStrings.Value;
        }
        public async Task<EmailEntity> GetEmailById(int emailTypeId)
        {
            using var connection = new SqlConnection(_connectionStrings.MainMik);
            var sqlCommand = "SELEFT * FROM [dbo].[EmailDictionary] WHERE [Id] = @emailTypeId";
            var result = await connection.QuerySingleOrDefaultAsync<EmailEntity>(sqlCommand, emailTypeId);
            return result;
        }
    }
}
