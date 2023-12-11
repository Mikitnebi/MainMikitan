namespace MainMikitan.Domain.Requests.Customer;

public class CreateOrUpdateCustomerInfoRequest
{
    public string? FullName { get; set; } = null!;
    public DateOnly? BirthDate { get; set; }
    public int GenderId { get; set; }
    public int NationalityId { get; set; }
}