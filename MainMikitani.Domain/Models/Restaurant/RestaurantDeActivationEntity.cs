namespace MainMikitan.Domain.Models.Restaurant;

public class RestaurantDeActivationEntity
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
    public int CreateUserId { get; set; }
    public int CreateAt { get; set; }
}