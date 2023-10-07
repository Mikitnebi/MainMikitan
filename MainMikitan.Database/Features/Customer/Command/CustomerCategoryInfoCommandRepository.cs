using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain.Models.Setting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace MainMikitan.Database.Features.Customer.Command
{
    public class CustomerCategoryInfoCommandRepository : ICustomerCategoryInfoCommandRepository
    {
        private readonly ConnectionStringsOptions _connectionString;
        public CustomerCategoryInfoCommandRepository(
            IOptions<ConnectionStringsOptions> connectionStrings
            )
        {
            _connectionString = connectionStrings.Value;
        }
        public async Task<bool> CreateOrUpdate(List<int> ids, int customerId)
        {
            using var connection = new SqlConnection(_connectionString.MainMik);
            {
                string deleteQuery = "DELETE FROM [dbo].[CustomerCategoryInfo] WHERE [CustomerId] = @customerId";
                await connection.ExecuteAsync(deleteQuery, new { customerId });


                string insertQuery = "INSERT INTO CustomerCategoryInfo (CustomerId, CategoryId) VALUES (@customerId, @categoryId)";
                foreach (int categoryId in ids)
                {
                    var result = await connection.ExecuteAsync(insertQuery, new { customerId, categoryId });
                    if (result == 0)
                        return false;
                }
                return true;
            }
        }
        public async Task Delete(int customerId)
        {
            using var connection = new SqlConnection(_connectionString.MainMik);
            {
                string deleteQuery = "DELETE FROM [dbo].[CustomerCategoryInfo] WHERE [CustomerId] = @customerId";
                await connection.ExecuteAsync(deleteQuery, new { customerId });
            }
        }
        public async Task<bool> Insert(List<int> ids, int customerId)
        {
            using var connection = new SqlConnection(_connectionString.MainMik);
            string insertQuery = "INSERT INTO CustomerCategoryInfo (CustomerId, CategoryId) VALUES (@customerId, @categoryId)";
            foreach (int categoryId in ids)
            {
                var result = await connection.ExecuteAsync(insertQuery, new { customerId, categoryId });
                if (result == 0)
                    return false;
            }
            return true;
        }
    }
}
