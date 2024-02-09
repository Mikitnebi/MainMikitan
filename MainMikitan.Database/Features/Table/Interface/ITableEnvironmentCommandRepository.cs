using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Models.Restaurant.TableManagement;

namespace MainMikitan.Database.Features.Table.Interface;

public interface ITableEnvironmentCommandRepository
{
    public Task<ResponseModel<TableEnvironmentEntity>> AddTableEnvironmentInfo(TableEnvironmentEntity request,
        CancellationToken cancellationToken);

    public Task<bool> SaveChanges();
}