using MainMikitan.Domain.Models.Setting;
using MainMikitan.Domain.Models.Customer;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using Dapper;
using MainMikitan.Database.Features.Customer.Interface;

namespace MainMikitan.Database.Features.Customer
{
    public class CustomerQueryRepository(IOptions<ConnectionStringsOptions> connectionStrings)
        : ICustomerQueryRepository
    {
        private readonly ConnectionStringsOptions _connectionString = connectionStrings.Value;

        public async Task<CustomerEntity?> GetById(int id, CancellationToken cancellationToken = default)
        {
            await using var connection = new SqlConnection(_connectionString.MainMik);
            const string sqlCommand = "SELECT * FROM [dbo].[Customers] WHERE [Id] = @id AND [StatusId] != '0'";
            return await connection.QueryFirstOrDefaultAsync<CustomerEntity>(sqlCommand, new { id });
        }
        public async Task<CustomerEntity?> GetByEmail(string email, CancellationToken cancellationToken = default)
        {
            await using var connection = new SqlConnection(_connectionString.MainMik);
            const string sqlCommand = "SELECT * FROM [dbo].[Customers] WHERE [EmailAddress] = @email AND [StatusId] != '0'";
            return await connection.QueryFirstOrDefaultAsync<CustomerEntity>(sqlCommand, new { email });
        }
        public async Task<CustomerEntity?> GetByMobileNumber(string mobileNumber, CancellationToken cancellationToken = default)
        {
            await using var connection = new SqlConnection(_connectionString.MainMik);
            const string sqlCommand = "SELECT * FROM [dbo].[Customers] WHERE [MobileNumber] = @mobileNumber AND [StatusId] != '0'";
            return await connection.QueryFirstOrDefaultAsync<CustomerEntity>(sqlCommand, new { mobileNumber });
        }
        public async Task<CustomerEntity?> GetNonVerifiedByEmail(string email, CancellationToken cancellationToken = default)
        {
            await using var connection = new SqlConnection(_connectionString.MainMik);
            const string sqlCommand = "SELECT * FROM [dbo].[Customers] WHERE [EmailAddress] = @email AND [StatusId] = '0' ORDER BY [CreatedAt] DESC";
            return await connection.QueryFirstOrDefaultAsync<CustomerEntity>(sqlCommand, new { email });
        }
    }
}
