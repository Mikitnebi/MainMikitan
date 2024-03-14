using MainMikitan.Domain.Models.Restaurant;

namespace MainMikitan.Application.Services.Permission;

public interface IPermissionService
{
    Task<bool> Check(int staffId, IEnumerable<int> permissionsList, string userRole, 
        CancellationToken cancellationToken = default,
        int restaurantId = -1,
        int subscriptionId = 0);
    Task<List<int>> GetByStaffId(int staffId, CancellationToken cancellationToken = default);
}