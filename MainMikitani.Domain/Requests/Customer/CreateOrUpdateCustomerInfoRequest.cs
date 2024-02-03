namespace MainMikitan.Domain.Requests.Customer;

public class CreateOrUpdateCustomerInfoRequest
{
    public DateOnly? BirthDate { get; set; }
    public int GenderId { get; set; }
    public int NationalityId { get; set; }
}