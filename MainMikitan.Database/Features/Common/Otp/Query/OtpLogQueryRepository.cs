using Dapper;
using MainMikitan.Database.Features.Common.Otp.Interfaces;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Common;
using MainMikitan.Domain.Models.Setting;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace MainMikitan.Database.Features.Common.Otp.Query
{
    public class OtpLogQueryRepository(IOptions<ConnectionStringsOptions> connectionStrings) : IOtpLogQueryRepository
    {
        private readonly ConnectionStringsOptions _connectionStrings = connectionStrings.Value;

        public async Task<OtpLogIntroEntity?> GetOtpByEmail(string email)
        {
            await using var connection = new SqlConnection(_connectionStrings.MainMik);
            const string sqlCommand = "SELECT * FROM [dbo].[OtpLogIntro] WHERE [EmailAddress] = @email  ORDER BY [CreatedAt] DESC";
            var result = await connection.QueryFirstOrDefaultAsync<OtpLogIntroEntity>(sqlCommand, new { email });
            return result;
        }

        
    }
}
