namespace MainMikitan.Domain.Requests.RestaurantRequests.Event;

public class CreateOrUpdateEventDetailsRequest
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public string NameGeo { get; set; } = null!;
    public string NameEng { get; set; } = null!;
    public string DescriptionGeo { get; set; } = null!;
    public string DescriptionEng { get; set; } = null!;
    public int MaxAttendances { get; set; }
    public bool NeedsRegistration { get; set; }
    public bool TakeManagersRegistrationAddress { get; set; }
    public string? EventAddressGeo { get; set; }
    public string? EventAddressEng { get; set; }
    public decimal? Price { get; set; }
    public bool HasMusician { get; set; }
    public string? Musician { get; set; }
    public bool HasPresenter { get; set; }
    public string? Presenter { get; set; }
    public bool HasDancer { get; set; }
    public string? Dancer { get; set; }
}