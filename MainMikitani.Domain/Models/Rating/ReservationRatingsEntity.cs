namespace MainMikitan.Domain.Models.Rating;

public class ReservationRatingsEntity
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public int UserId { get; set; }
    public int ReservationId { get; set; }
    public float OverallRestaurantRating { get; set; }
    public float? OverallAppRating { get; set; }
    public float? OverallDishRating { get; set; }
    public float? EnvironmentRating { get; set; }
    public float? ServiceRating { get; set; }
    public float? PriceRating { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
}