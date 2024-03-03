namespace MainMikitan.Domain.Requests.Rating;

public class SaveCustomerRatingRequest
{
    public int CustomerId { get; set; }
    public int ReservationId { get; set; }
    public float Rating { get; set; }
}