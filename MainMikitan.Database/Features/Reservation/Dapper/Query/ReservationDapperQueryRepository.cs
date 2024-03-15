using System.Data.SqlClient;
using Dapper;
using MainMikitan.Database.Features.Reservation.Dapper.Interface;
using MainMikitan.Domain.Models.Reservation;
using MainMikitan.Domain.Models.Setting;
using Microsoft.Extensions.Options;

namespace MainMikitan.Database.Features.Reservation.Dapper.Query;

public class ReservationDapperQueryRepository(IOptions<ConnectionStringsOptions> connectionStringOptions) : IReservationDapperQueryRepository
{
    private ConnectionStringsOptions ConnectionStrings { get; }= connectionStringOptions.Value;
    public async Task<List<ReservationEntity>?> GetActiveReservation()
    {
        await using var sqlConnection = new SqlConnection(ConnectionStrings.MainMik);
        var sqlQuery = "SELECT * FROM [dbo].[Reservations] WHERE IsCompleted = FALSE";
        var executeResponse = await sqlConnection.ExecuteScalarAsync<List<ReservationEntity>>(sqlQuery);
        return executeResponse;
    }
}