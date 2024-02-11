using MainMikitan.Domain.Models.Restaurant;

namespace MainMikitan.Application.Services.Permission;

public interface IPermissionService
{
    Task<bool> Check(int staffId, IEnumerable<int> permissionsList, CancellationToken cancellationToken = default);
    Task<List<int>> GetByStaffId(int staffId, CancellationToken cancellationToken = default);
}