namespace MainMikitan.Domain.Models.Restaurant;

public class RestaurantSubscriptionAndPermissionMapEntity
{
    public int Id { get; set; }
    public int SubscriptionTypeId { get; set; }
    public int PermissionId { get; set; }
    public DateTime CreatedAt { get; set; }
}