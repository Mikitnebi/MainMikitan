using MainMikitan.Domain.Models.Restaurant.TableManagement;

namespace MainMikitan.Database.Features.Reservation.Dapper.Interface;

public interface ITableDapperQueryRepository
{
    Task<List<TableInfoEntity>?> GetAllActiveTable(CancellationToken cancellationToken = default);
}