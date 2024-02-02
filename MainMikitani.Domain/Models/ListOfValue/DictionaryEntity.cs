namespace MainMikitan.Domain.Models.ListOfValue;

public record DictionaryEntity
{
    public int Id { get; set; }
    public string NameGeo { get; set; } = null!;
    public string NameEng { get; set; } = null!;
    public int SectorId { get; set; }
}