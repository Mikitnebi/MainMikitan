using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using FluentEmail.Core;
using MainMikitan.Domain.Models.Customer;
using MainMikitan.Domain.Models.Setting;
using Microsoft.Extensions.Options;
using System.Data.Common;

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

            string sqlCommand = $"SELECT Id FROM [dbo].[Category] WITH (NOLOCK) WHERE Id IN (@indexes) AND [Status] = 1";
            dynamic sqlResult = await connection.QueryAsync<int>(sqlCommand, indexes);
            sqlResult = sqlResult.ToList();

            return sqlResult;
        }
    }
}
