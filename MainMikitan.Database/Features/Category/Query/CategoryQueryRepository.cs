using System.Data.SqlClient;
using Dapper;
using MainMikitan.Domain.Models.Setting;
using Microsoft.Extensions.Options;

namespace MainMikitan.Database.Features.Category.Query
{
    public class CategoryQueryRepository(IOptions<ConnectionStringsOptions> connectionStrings)
        : ICategoryQueryRepository
    {
        private readonly ConnectionStringsOptions _connectionString = connectionStrings.Value;

        public async Task<List<int>> GetAllActive(List<int> indexes)
        {
            await using var connection = new SqlConnection(_connectionString.MainMik);
            
            var sqlCommand = $"SELECT Id FROM [dbo].[Category] WITH (NOLOCK) WHERE Id IN (@indexes) AND [StatusId] = 1";
            
            var parameters = new DynamicParameters();
            parameters.Add("indexes",indexes);
            
            var sqlResult = await connection.QueryAsync<int>(sqlCommand, parameters);
            return sqlResult.ToList();
        }
    }
}
