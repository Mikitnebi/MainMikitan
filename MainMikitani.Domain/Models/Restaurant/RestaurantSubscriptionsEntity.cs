namespace MainMikitan.Domain.Models.Restaurant;

public class RestaurantSubscriptionsEntity
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public int RestaurantSubscriptionTypeId { get; set; }
    public DateTime SubscriptionActivationDate { get; set; }
    public DateTime SubscriptionDeactivationDate { get; set; }
    public DateTime CreatedAt { get; set; }
}