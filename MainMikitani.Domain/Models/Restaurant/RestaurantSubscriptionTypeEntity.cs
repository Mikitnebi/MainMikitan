namespace MainMikitan.Domain.Models.Restaurant;

public class RestaurantSubscriptionTypeEntity
{
    public int Id { get; set; }
    public int SubscriptionTypeName { get; set; }
    public int PermissionListId { get; set; }
    public DateTime CreatedAt { get; set; }
}