using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.Table.Interface;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Models.Restaurant.TableManagement;
using MainMikitan.Domain.Templates;
using Microsoft.EntityFrameworkCore;

namespace MainMikitan.Database.Features.Table.Query;

public class TableQueryRepository(MikDbContext db) : ResponseMaker<TableInfoEntity>, ITableQueryRepository
{
    public async Task<ResponseModel<TableInfoEntity>> GetSingleTableByNumeration(int tableNumber, 
        int restaurantId,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await db.TableInfo
                .FirstOrDefaultAsync(ti => ti.TableNumber == tableNumber && ti.RestaurantId == restaurantId, cancellationToken: cancellationToken);

            return result is null ? Fail("TABLE_NOT_FOUND") : Success(result);
        }
        catch (Exception e)
        {
            return Unexpected(e);
        }
    }

    public async Task<List<TableInfoEntity>> GetAllTable(CancellationToken cancellationToken = default)
    {
        return await db.TableInfo.ToListAsync(cancellationToken);
    }
    
    public async Task<bool> SaveChanges() 
    {
        var result = await db.SaveChangesAsync();
        
        return result > 0;
    }
}