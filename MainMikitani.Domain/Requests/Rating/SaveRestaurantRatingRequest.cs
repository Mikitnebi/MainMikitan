namespace MainMikitan.Domain.Requests.Rating;

public class SaveRestaurantRatingRequest
{
    public int RestaurantId { get; set; }
    public int ReservationId { get; set; }
    public float Rating { get; set; }
}