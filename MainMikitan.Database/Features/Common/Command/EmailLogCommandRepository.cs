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

namespace MainMikitan.Database.Features.Common.Command
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
        public async Task<int> Create(int typeId, int userId, int userTypeId)
        {
            using var connection = new SqlConnection(_connectionStrings.MainMik);
            var emailLogEntity = new EmailLogEntity
            {
                UserId = userId,
                UserTypeId = userTypeId,
                EmailTypeId = typeId,
                CreateAt = DateTime.Now
            };
            var sqlCommand = "INSERT INTO [dbo].[EmailLog] " +
              "([UserId]," +
              "[UserTypeId]," +
              "[EmailTypeId]," +
              "[CreatedAt]," +
              " OUTPUT INSERTED.Id" +
              " VALUES (@UserId,@UserTypeId,@EmailTypeId, @CreatedAt)";
            var result = await connection.QuerySingleOrDefaultAsync<int>(sqlCommand, emailLogEntity);
            return result;
        }
    }
}
