namespace MainMikitan.Domain.Requests.Filter;

public record FilterRequestModel(
    List<int>? CustomerInterests = null,
    int? RegionId = null, 
    int? BusinessTypeId = null, 
    bool? HasCoupe = null, 
    bool? HasTerrace = null,
    int? IsActiveInSomeHour = null,
    int? IsActiveKitchenInSomeHour = null, 
    int? IsActiveMusicInSomeHour = null,
    int? PriceTypeId = null, 
    int? Rate = null, 
    int? PriorityTypeId = null);