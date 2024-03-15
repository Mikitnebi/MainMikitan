using MainMikitan.Database.Features.Reservation.Dapper.Interface;
using MainMikitan.Domain.Models.Restaurant.TableManagement;

namespace MainMikitan.Database.Features.Reservation.Dapper.Query;

public class TableDapperEnvironmentQueryRepository : ITableDapperEnvironmentQueryRepository
{
    public Task<List<TableEnvironmentEntity>> GetAllEnvironment()
    {
        throw new NotImplementedException();
    }
}