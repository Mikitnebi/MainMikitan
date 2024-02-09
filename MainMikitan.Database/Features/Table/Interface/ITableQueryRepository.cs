using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Models.Restaurant.TableManagement;

namespace MainMikitan.Database.Features.Table.Interface;

public interface ITableQueryRepository
{
    public Task<ResponseModel<TableInfoEntity>> GetSingleTableByNumeration(int tableNumber, int restaurantId, CancellationToken cancellationToken);
    public Task<bool> SaveChanges();
}