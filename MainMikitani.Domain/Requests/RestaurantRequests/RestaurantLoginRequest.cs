namespace MainMikitan.Domain.Requests.RestaurantRequests;

public class RestaurantLoginRequest
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}
