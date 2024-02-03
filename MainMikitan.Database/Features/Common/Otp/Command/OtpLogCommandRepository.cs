using Dapper;
using MainMikitan.Database.Features.Common.Otp.Interfaces;
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
    public class OtpLogCommandRepository(IOptions<ConnectionStringsOptions> connectionStrings)
        : IOtpLogCommandRepository
    {
        private readonly ConnectionStringsOptions _connectionStrings = connectionStrings.Value;

        public async Task<bool> Create(OtpLogIntroEntity model, CancellationToken cancellationToken = default)
        {
            await using var connection = new SqlConnection(_connectionStrings.MainMik);
            model.CreatedAt = DateTime.Now;
            model.StatusId = (int)OtpStatusId.NoneVerified;
            const string sqlCommand = $"""
                                       INSERT INTO [dbo].[OtpLogIntro] 
                                      ([CreatedAt], 
                                      [Otp],
                                      [MobileNumber], 
                                      [EmailAddress], 
                                      [NumberOfTrials], 
                                      [NumberOfTrialsIsRequired],
                                      [StatusId],
                                      [ValidationTime],
                                      [UserTypeId],
                                      [OperationId]) 
                                       OUTPUT INSERTED.Id
                                       VALUES (@CreatedAt, @Otp, @MobileNumber, @EmailAddress, @NumberOfTrials, @NumberOfTrialsIsRequired, @StatusId, @ValidationTime, @UserTypeId, @OperationId)
                                      """;
            var result = await connection.QuerySingleOrDefaultAsync<int?>(sqlCommand, model);
            return result > 0;
        }
        public async Task<bool> Update(int id, int numberOfTrials, int status, CancellationToken cancellationToken = default)
        {
            var otpStatus = (int)status;
            await using var connection = new SqlConnection(_connectionStrings.MainMik);

            const string sqlCommand = "UPDATE [dbo].[OtpLogIntro] " +
                                      "SET [NumberOfTrials] = @numberOfTrials, " +
                                      "[StatusId] = @otpStatus " +
                                      " WHERE [Id] = @id";
            var result = await connection.ExecuteAsync(sqlCommand, new {id, numberOfTrials, otpStatus});
            return result > 0;

        }
    }
}
