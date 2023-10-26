namespace MainMikitan.Domain.Requests.Customer;

public class CreateOrUpdateCustomerInfoRequest
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateOnly? BirthDate { get; set; }
    public int GenderId { get; set; }
    public int NationalityId { get; set; }
}