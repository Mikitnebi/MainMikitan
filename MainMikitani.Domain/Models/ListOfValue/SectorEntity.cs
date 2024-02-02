namespace MainMikitan.Domain.Models.ListOfValue;

public record SectorEntity
{
    public int Id { get; set; }
    public string NameEng { get; set; } = null!;
    public string NameGeo { get; set; } = null!;
}