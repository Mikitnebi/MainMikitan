using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Models.Restaurant.TableManagement;

namespace MainMikitan.Database.Features.Table.Interface;

public interface ITableCommandRepository
{
    public Task<ResponseModel<TableInfoEntity>> AddTable(TableInfoEntity request, CancellationToken cancellationToken);
    public Task<bool> SaveChanges();
}