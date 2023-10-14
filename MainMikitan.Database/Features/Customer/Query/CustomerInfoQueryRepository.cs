using System.Data.SqlClient;
using Dapper;
using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain.Models.Customer;
using MainMikitan.Domain.Models.Setting;
using Microsoft.Extensions.Options;

namespace MainMikitan.Database.Features.Customer.Query;

public class CustomerInfoQueryRepository : ICustomerInfoQueryRepository
{
    private readonly ConnectionStringsOptions _connectionString;
    public CustomerInfoQueryRepository(
        IOptions<ConnectionStringsOptions> connectionStrings
    )
    {
        _connectionString= connectionStrings.Value;
    }
    
    public async Task<CustomerInfoEntity?> GetVerifiedFromCustomerInfoById(int customerId)
    {
        using var connection = new SqlConnection(_connectionString.MainMik);

        var sqlCommand = "SELECT * FROM [dbo].[CustomersInfo] WHERE [CustomerId] = @customerId";
        return await connection.QueryFirstOrDefaultAsync<CustomerInfoEntity>(sqlCommand, new { customerId });
    }
}

public class CustomerInfoEntity
{
}