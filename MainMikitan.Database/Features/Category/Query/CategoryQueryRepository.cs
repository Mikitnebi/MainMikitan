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
        public async Task<int> GetMaxIndex()
        {
            using var connection = new SqlConnection(_connectionString.MainMik);

            string sqlCommand = "SELECT MAX(Id) FROM [dbo].[Category] (NOLOCK)";
            return await connection.ExecuteScalarAsync<int>(sqlCommand);
        }
    }
}
