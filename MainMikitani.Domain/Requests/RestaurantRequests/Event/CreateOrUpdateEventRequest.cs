namespace MainMikitan.Domain.Requests.RestaurantRequests.Event;

public class CreateOrUpdateEventRequest
{
    public int? Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}