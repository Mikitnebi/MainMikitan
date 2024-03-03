namespace MainMikitan.Domain.Models.Rating;

public class CustomerRatingEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RestaurantId { get; set; }
    public int ReservationId { get; set; }
    public float Rating { get; set; }
    public DateTime CreatedAt { get; set; }
}