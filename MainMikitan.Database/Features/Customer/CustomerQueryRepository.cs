using MainMikitan.Domain.Models.Setting;
using MainMikitan.Domain.Models.Customer;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MainMikitan.Domain.Interfaces.Customer;

namespace MainMikitan.Database.Features.Customer
{
    public class CustomerQueryRepository : ICustomerQueryRepository
    {
        private readonly ConnectionStringsOptions _connectionString;
        public CustomerQueryRepository(
            IOptions<ConnectionStringsOptions> connectionStrings
            )
        {
            _connectionString= connectionStrings.Value;
        }
        public async Task<CustomerEntity> GetByEmail(string email)
        {
            using var connection = new SqlConnection(_connectionString.MainMik);

            var sqlCommand = "SELECT * FROM [dbo].[Operators] WHERE [Email] = @email";
            return await connection.QueryFirstOrDefaultAsync<CustomerEntity>(sqlCommand, new { email });
        }
    }
}
