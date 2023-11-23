namespace MainMikitan.Domain.Requests.Customer.Auth;

public record CustomerAuthRequestModel
{
    public string EmailAdress { get; set; } = null!;
    public int Id { get; set; }
}
