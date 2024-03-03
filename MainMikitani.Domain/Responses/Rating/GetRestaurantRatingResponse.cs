namespace MainMikitan.Domain.Responses.Rating;

public class GetRestaurantRatingResponse
{
    public int RestaurantId { get; set; }
    public float Rating { get; set; }
}