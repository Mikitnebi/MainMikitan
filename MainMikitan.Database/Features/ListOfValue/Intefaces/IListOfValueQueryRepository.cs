using MainMikitan.Domain.Models.ListOfValue;

namespace MainMikitan.Database.Features.ListOfValue.Intefaces;

public interface IListOfValueQueryRepository
{
    Task<List<DictionaryEntity>> GetDictionaryBySectorId
    (
        int sectorId,
        CancellationToken cancellationToken = default
    );

    Task<SectorEntity> GetSectorById
    (
        int id,
        CancellationToken cancellationToken = default
    );
}