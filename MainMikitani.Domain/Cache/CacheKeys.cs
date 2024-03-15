namespace MainMikitan.Domain.Cache;

public record CacheKeys
{
    #region Customer
    public static string CustomerInfo(int id) =>  $"customer-{id}-info";
    public static string CustomerProfileImageUrl(int id) => $"customer-{id}-profile-image-url";
    public static string CustomerInterests(int id) => $"customer-{id}-interests";
    #endregion
    
    #region Restaurant
    public static string RestaurantInfo(int id) =>  $"restaurant-{id}-info";
    public static string RestaurantProfileImageUrl(int id) => $"restaurant-{id}-profile-image-url";
    public static string RestaurantEnvironmentImageUrls(int id) => $"restaurant-{id}-environment-image-url";
    public static string RestaurantEnvironment(int id) => $"restaurant-{id}-environment";
    public static string RestaurantSubscription(int restaurantId) => $"restaurant-{restaurantId}-subscription";
    #endregion
}
