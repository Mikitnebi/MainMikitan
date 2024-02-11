using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain.Models.Restaurant;
using Microsoft.EntityFrameworkCore;

namespace MainMikitan.Database.Features.Restaurant.Command;

public class StaffPermissionQueryRepository(MikDbContext db) : IStaffPermissionQueryRepository
{
    public async Task<List<PermissionEntity>> GetPermissionByStaffId(int staffId, CancellationToken cancellationToken = default)
    {
        return await db.Permission.Where(t => t.StaffId == staffId).ToListAsync(cancellationToken);
    }
}