namespace MainMikitan.Domain.Responses.Filter;

public record RestaurantInfoResponse
{
    public int RestaurantId { get; set; }
    public double LocationX { get; set; }
    public double LocationY { get; set; }
    public int PriceTypeId { get; set; }
    public string Address { get; set; } = null!;
    public int BusinessTypeId { get; set; }
    public bool HasCoupe { get; set; }
    public bool HasTerrace { get; set; }
    public short HallStartTime { get; set; }
    public short HallEndTime { get; set; }
    public short KitchenStartTime { get; set; }
    public short KitchenEndTime { get; set; }
    public short MusicStartTime { get; set; }
    public short MusicEndTime { get; set; }
    public bool ForCorporateEvents { get; set; }
    public string? DescriptionGeo { get; set; }
    public string? DescriptionEng { get; set; }
}