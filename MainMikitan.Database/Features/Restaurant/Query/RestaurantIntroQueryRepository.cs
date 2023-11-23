using Dapper;
using MainMikitan.Domain.Interfaces.Restaurant;
using MainMikitan.Domain.Models.Restaurant;
using MainMikitan.Domain.Models.Setting;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace MainMikitan.Database.Features.Restaurant.Query {
    public class RestaurantIntroQueryRepository : IRestaurantIntroQueryRepository {
        private readonly ConnectionStringsOptions _connectionString;
        public RestaurantIntroQueryRepository(
            IOptions<ConnectionStringsOptions> connectionStrings
            ) 
        {
            _connectionString = connectionStrings.Value;
        }

        public async Task<RestaurantEntity> GetByUsername(string username)
        {
            using var connection = new SqlConnection(_connectionString.MainMik);

            var sqlCommand = "SELECT * FROM [dbo].[Restaurant] WHERE [Username] = @username";

            return await connection.QueryFirstOrDefaultAsync<RestaurantEntity>(sqlCommand, new { username });
        }

        public async Task<RestaurantIntroEntity> GetNonVerifiedByEmail(string email) {
            using var connection = new SqlConnection(_connectionString.MainMik);

            var sqlCommand = "SELECT * FROM [dbo].[RestaurantIntro] WHERE [EmailAddress] = @email AND [RestaurantOtpVerificationId] = '0' ORDER BY [CreatedAt] DESC";
            return await connection.QueryFirstOrDefaultAsync<RestaurantIntroEntity>(sqlCommand, new { email });
        }

        public async Task<RestaurantIntroEntity> GetVerifiedByEmail(string email)
        {
            using var connection = new SqlConnection(_connectionString.MainMik);

            var sqlCommand = "SELECT * FROM [dbo].[RestaurantIntro] WHERE [EmailAddress] = @email AND [RestaurantOtpVerificationId] = '1' ORDER BY [CreatedAt] DESC";
            return await connection.QueryFirstOrDefaultAsync<RestaurantIntroEntity>(sqlCommand, new { email });
        }
    }
}
