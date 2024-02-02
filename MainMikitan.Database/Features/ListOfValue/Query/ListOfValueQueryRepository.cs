using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.ListOfValue.Intefaces;
using MainMikitan.Domain.Models.ListOfValue;
using Microsoft.EntityFrameworkCore;

namespace MainMikitan.Database.Features.ListOfValue.Query;

public class ListOfValueQueryRepository(MikDbContext mikDbContext) :IListOfValueQueryRepository
{
    public async Task<List<DictionaryEntity>> GetDictionaryBySectorId
        (
        int sectorId,
        CancellationToken cancellationToken = default
        )
    {
        return await mikDbContext.Dictionary.Where(t => t.SectorId == sectorId).AsNoTracking().ToListAsync(cancellationToken);
    }
    
    public async Task<SectorEntity> GetSectorById
    (
        int id,
        CancellationToken cancellationToken = default
    )
    {
        return await mikDbContext.Sector.Where(t => t.Id == id).AsNoTracking().FirstAsync(cancellationToken);
    }
}