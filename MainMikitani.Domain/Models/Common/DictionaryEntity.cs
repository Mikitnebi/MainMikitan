namespace MainMikitan.Domain.Models.Common;

public class DictionaryEntity
{
    public int Id { get; set; }
    public int SectorId { get; set; }
    public string GeoName { get; set; } = null!;
    public string EngName { get; set; } = null!;
}