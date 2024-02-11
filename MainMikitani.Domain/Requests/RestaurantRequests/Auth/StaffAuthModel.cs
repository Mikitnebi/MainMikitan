namespace MainMikitan.Domain.Requests.RestaurantRequests.Auth;

public class StaffAuthModel
{
    public int StaffId { get; set; }
    public int RestaurantId { get; set; }
    public bool IsManager { get; set; }
    public List<int>? Permissions { get; set; }
}
