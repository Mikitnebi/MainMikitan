using Dapper;
using MainMikitan.Database.Features.Reservation.Dapper.Interface;
using MainMikitan.Domain.Models.Restaurant;
using MainMikitan.Domain.Models.Setting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace MainMikitan.Database.Features.Reservation.Dapper.Query;

public class RestaurantDeActivationDapperQueryRepository(IOptions<ConnectionStringsOptions> connectionStringsOptions) 
    : IRestaurantDeActivationDapperQueryRepository
{
    private ConnectionStringsOptions ConnectionStrings { get; }= connectionStringsOptions.Value;
    public async Task<List<RestaurantDeActivationEntity>?> GetAll()
    {
        await using var sqlConnection = new SqlConnection(ConnectionStrings.MainMik);
        var sqlQuery = "SELECT * FROM [dbo].[RestaurantDeActivation] WHERE EndAt > GETDATE()";
        var executeResponse = await sqlConnection.ExecuteScalarAsync<List<RestaurantDeActivationEntity>>(sqlQuery);
        return executeResponse;
    }
}