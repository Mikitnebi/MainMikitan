namespace MainMikitan.Domain.Models.Restaurant;

public class RestaurantSubscriptionInfoEntity
{
    public int PermissionId { get; set; }
    public DateTime SubscriptionActivationDate { get; set; }
    public DateTime SubscriptionDeactivationDate { get; set; }
    public DateTime CacheAddDate { get; set; }
}