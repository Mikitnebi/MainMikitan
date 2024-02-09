using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.Table.Interface;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Models.Restaurant.TableManagement;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Database.Features.Table.Command;

public class TableCommandRepository : ResponseMaker<TableInfoEntity>, ITableCommandRepository
{
    private readonly MikDbContext _db;

    public TableCommandRepository(MikDbContext db)
    {
        _db = db;
    }
        
    public async Task<ResponseModel<TableInfoEntity>> AddTable(TableInfoEntity request,
        CancellationToken cancellationToken)
    {
        try
        {
            request.CreatedAt = DateTime.Now;
            
            var result = await _db.TableInfo.AddAsync(request, cancellationToken);


            return Success(result.Entity);
        }
        catch (Exception e)
        {
            return Unexpected(e);
        }
    }
    
    public async Task<bool> SaveChanges() {
        var result = await _db.SaveChangesAsync();
        
        return result > 0;
    }
}