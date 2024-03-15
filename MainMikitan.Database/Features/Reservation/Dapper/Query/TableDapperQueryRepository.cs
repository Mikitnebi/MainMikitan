using Dapper;
using MainMikitan.Database.Features.Reservation.Dapper.Interface;
using MainMikitan.Domain.Models.Restaurant.TableManagement;
using MainMikitan.Domain.Models.Setting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace MainMikitan.Database.Features.Reservation.Dapper.Query;

public class TableDapperQueryRepository(IOptions<ConnectionStringsOptions> connectionString) : ITableDapperQueryRepository
{
    private ConnectionStringsOptions ConnectionString { get; } = connectionString.Value;
    public async Task<List<TableInfoEntity>?> GetAllActiveTable(CancellationToken cancellationToken = default)
    {
        await using var sqlConnection = new SqlConnection(ConnectionString.MainMik);
        var sqlQuery = "SELECT * FROM [dbo].[TableInfo] WHERE Status = TRUE";
        var sqlQueryResponse = await sqlConnection.ExecuteScalarAsync<List<TableInfoEntity>>(sqlQuery);
        return sqlQueryResponse;
    }
}