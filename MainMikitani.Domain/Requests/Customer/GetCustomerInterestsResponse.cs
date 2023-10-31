namespace MainMikitan.Domain.Requests.Customer;

public record GetCustomerInterestsResponse
{
    public List<int>? InterestsIds { get; set; }
}