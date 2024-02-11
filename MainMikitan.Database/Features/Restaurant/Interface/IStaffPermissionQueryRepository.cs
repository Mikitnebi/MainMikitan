using MainMikitan.Domain.Models.Restaurant;

namespace MainMikitan.Database.Features.Restaurant.Interface;

public interface IStaffPermissionQueryRepository
{
    Task<List<PermissionEntity>> GetPermissionByStaffId(int staffId, CancellationToken cancellationToken = default);
}