using Dapper;
using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain.Models.Setting;
using MainMikitan.Domain.Responses.Customer;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace MainMikitan.Database.Features.Customer.Query;

public class CustomerInterestQueryRepository : ICustomerInterestQueryRepository
{
    private readonly ConnectionStringsOptions _connectionString;
    public CustomerInterestQueryRepository(
        IOptions<ConnectionStringsOptions> connectionStrings
    )
    {
        _connectionString= connectionStrings.Value;
    }

    public async Task<IEnumerable<CustomerInterestResponse>> GetAllCustomerInterest(int customerId)
    {
        using var connection = new SqlConnection(_connectionString.MainMik);
        
        
        var sqlCommand = "SELECT * FROM [MainMikitan].[dbo].[CustomerInterest]";
        var parameters = new DynamicParameters();
        parameters.Add("@customerId", customerId);

        return await connection.QueryAsync<CustomerInterestResponse>(sqlCommand);
    }
}