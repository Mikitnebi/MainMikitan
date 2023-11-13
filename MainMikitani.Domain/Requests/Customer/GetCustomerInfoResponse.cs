namespace MainMikitan.Domain.Requests.Customer;

public record GetCustomerInfoResponse
{
    public string FullName { get; set; } = null!;
    public DateOnly BirthDate { get; set; }
    public int GenderId { get; set; }
    public int NationalityId { get; set; }
    public string ImageUrl { get; set; } = null!;
}