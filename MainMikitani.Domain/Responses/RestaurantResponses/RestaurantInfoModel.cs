namespace MainMikitan.Domain.Responses.RestaurantResponses;

public class RestaurantInfoModel
{
    public decimal LocationX { get; set; }
    public decimal LocationY { get; set; }
    public int PriceTypeId { get; set; }
    public string Address { get; set; } = null!;
    public string AddressEng { get; set; } = null!;
    public int RegionId { get; set; }
    public decimal Rate { get; set; }
    public int RateNumber { get; set; }
    public int BusinessTypeId { get; set; }
    public bool HasCoupe { get; set; }
    public bool HasTerrace { get; set; }
    public TimeOnly HallStartTime { get; set; }
    public TimeOnly HallEndTime { get; set; }
    public TimeOnly KitchenStartTime { get; set; }
    public TimeOnly KitchenEndTime { get; set; }
    public TimeOnly MusicStartTime { get; set; }
    public TimeOnly MusicEndTime { get; set; }
    public bool ForCorporateEvents { get; set; }
    public string? DescriptionGeo { get; set; }
    public string? DescriptionEng { get; set; }
    public int ActiveStatusId { get; set; }
}
