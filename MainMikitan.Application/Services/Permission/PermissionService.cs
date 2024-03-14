using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Restaurant;

namespace MainMikitan.Application.Services.Permission;

public class PermissionService(IStaffPermissionQueryRepository staffPermissionQueryRepository,
    IRestaurantSubscriptionRepository restaurantSubscriptionRepository)
    : IPermissionService
{
    public async Task<bool> Check(int staffId, IEnumerable<int> permissionsList, string userRole, CancellationToken cancellationToken = default, int restaurantId = -1, int subscriptionId = 0)
    {
        if (userRole == Enums.RoleId.Manager.ToString()) return true;
        var staffPermission = await staffPermissionQueryRepository.GetPermissionByStaffId(staffId, cancellationToken);
        if (restaurantId < 0)
        {
            return permissionsList.Any(permission => staffPermission.Any(t => t.PermissionId == permission));
        }
        var restaurantSubscriptionType = await restaurantSubscriptionRepository.GetSubscription(restaurantId, cancellationToken);
        return permissionsList.Any(permission =>
            staffPermission.Any(t => t.PermissionId == permission) &&
            restaurantSubscriptionType!.RestaurantSubscriptionTypeId > subscriptionId);
    }
    public async Task<List<int>> GetByStaffId(int staffId, CancellationToken cancellationToken = default)
    {
        var permission = await staffPermissionQueryRepository.GetPermissionByStaffId(staffId, cancellationToken);
        return permission.Select(t => t.PermissionId ).ToList();
    }
}