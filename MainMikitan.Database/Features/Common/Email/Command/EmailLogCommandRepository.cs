using Dapper;
using MainMikitan.Domain.Interfaces.Common;
using MainMikitan.Domain.Models.Common;
using MainMikitan.Domain.Models.Setting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Database.Features.Common.Email.Command
{
    public class EmailLogCommandRepository : IEmailLogCommandRepository
    {
        private readonly ConnectionStringsOptions _connectionStrings;
        public EmailLogCommandRepository(
            IOptions<ConnectionStringsOptions> connectionStrings
            )
        {
            _connectionStrings = connectionStrings.Value;
        }
        public async Task<int?> Create(string email, int typeId, int userId, int userTypeId, string data)
        {
            using var connection = new SqlConnection(_connectionStrings.MainMik);
            var emailLogEntity = new EmailLogEntity
            {
                EmailAddress = email,
                UserId = userId,
                UserTypeId = userTypeId,
                EmailTypeId = typeId,
                CreatedAt = DateTime.Now,
                Data = data
            };
            var sqlCommand = "INSERT INTO [dbo].[EmailLog] " +
              "([UserId]," +
              "[UserTypeId]," +
              "[EmailTypeId]," +
              "[CreatedAt]," +
              "[EmailAddress]," +
              "[Data])" +
              " OUTPUT INSERTED.Id" +
              " VALUES ( @UserId,@UserTypeId,@EmailTypeId, @CreatedAt, @EmailAddress, @Data)";
            var result = await connection.QuerySingleOrDefaultAsync<int?>(sqlCommand, emailLogEntity);
            return result;
        }
    }
}
