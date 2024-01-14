using System.Data.SqlClient;
using Dapper;
using MainMikitan.Domain.Models.Setting;
using Microsoft.Extensions.Options;

namespace MainMikitan.Database.Features.Category.Query
{
    public class CategoryQueryRepository : ICategoryQueryRepository
    {
        private readonly ConnectionStringsOptions _connectionString;
        public CategoryQueryRepository(IOptions<ConnectionStringsOptions> connectionStrings) 
        { 
            _connectionString = connectionStrings.Value;
        }
        public async Task<List<int>> GetAllActive(List<int> indexes)
        {
            using var connection = new SqlConnection(_connectionString.MainMik);
            
            string sqlCommand = $"SELECT Id FROM [dbo].[Category] WITH (NOLOCK) WHERE Id IN (@indexes) AND [StatusId] = 1";
            
            var parameters = new DynamicParameters();
            parameters.Add("indexes",indexes);
            
            dynamic sqlResult = await connection.QueryAsync<int>(sqlCommand, parameters);
            sqlResult = sqlResult.ToList();

            return sqlResult;
        }
    }
}
