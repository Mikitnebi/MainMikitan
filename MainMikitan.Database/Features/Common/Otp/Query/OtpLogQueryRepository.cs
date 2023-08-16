using Dapper;
using MainMikitan.Database.Features.Common.Otp.Interfaces;
using MainMikitan.Domain;
using MainMikitan.Domain.Interfaces.Common;
using MainMikitan.Domain.Models.Common;
using MainMikitan.Domain.Models.Setting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Database.Features.Common.Otp.Query
{
    public class OtpLogQueryRepository : IOtpLogQueryRepository
    {
        private readonly ConnectionStringsOptions _connectionStrings;
        public OtpLogQueryRepository(IOptions<ConnectionStringsOptions> connectionStrings)
        {
            _connectionStrings = connectionStrings.Value;
        }

        public async Task<OtpLogIntroEntity> GetOtpbyEmail(string email)
        {
            using var connection = new SqlConnection(_connectionStrings.MainMik);
            var sqlCommand = "SELECT * FROM [dbo].[OtpLogIntro] WHERE [EmailAddress] = @email " +
                "ORDER BY [CreatedAt] DESC";
            var result = await connection.QueryFirstOrDefaultAsync<OtpLogIntroEntity>(sqlCommand, new { email });
            return result;
        }

        
    }
}
