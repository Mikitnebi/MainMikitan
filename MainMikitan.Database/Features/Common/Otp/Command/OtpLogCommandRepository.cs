using Dapper;
using MainMikitan.Database.Features.Common.Otp.Interfaces;
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

namespace MainMikitan.Database.Features.Common.Otp.Command
{
    public class OtpLogCommandRepository : IOtpLogCommandRepository
    {
        private readonly ConnectionStringsOptions _connectionStrings;
        public OtpLogCommandRepository(IOptions<ConnectionStringsOptions> connectionStrings)
        {
            _connectionStrings = connectionStrings.Value;
        }
        public async Task<int?> Create(OtpLogIntroEntity model)
        {
            using var connection = new SqlConnection(_connectionStrings.MainMik);
            model.CreatedAt = DateTime.Now;
            model.StatusId = (int)OtpStatusId.NoneVerified;
            var sqlCommand = "INSERT INTO [dbo].[OtpLogIntro] " +
              "([CreatedAt], " +
              "[Otp]," +
              "[MobileNumber], " +
              "[EmailAddress], " +
              "[NumberOfTrials], " +
              "[NumberOfTrialsIsRequired]," +
              "[StatusId]," +
              "[ValidationTime]," +
              "[UserTypeId]) " +
              " OUTPUT INSERTED.Id" +
              " VALUES (@CreatedAt, @Otp, @MobileNumber, @EmailAddress, @NumberOfTrials, @NumberOfTrialsIsRequired, @StatusId, @ValidationTime, @UserTypeId)";
            var result = await connection.QuerySingleOrDefaultAsync<int?>(sqlCommand, model);
            return result;
        }
        public async Task<int?> Update(int id, int numberOfTrials, OtpStatusId status)
        {
            var otpStatus = (int)status;
            using var connection = new SqlConnection(_connectionStrings.MainMik);

            var sqlCommand = "UPDATE [dbo].[OtpLogIntro] " +
                "SET [NumberOfTrials] = @numberOfTrials, " +
                "[StatusId] = @otpStatus " +
                " WHERE [Id] = @id";
            var result = await connection.ExecuteAsync(sqlCommand, new {id, numberOfTrials, otpStatus});
            return result;

        }
    }
}
