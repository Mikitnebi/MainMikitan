namespace MainMikitan.Domain.Requests.RestaurantRequests;

public class StaffLoginRequest
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}
