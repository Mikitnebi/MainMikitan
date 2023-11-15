namespace MainMikitan.Domain.Models.Restaurant;

public class RestaurantEnvironmentInfoEntity
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public int EnvironmentId { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}