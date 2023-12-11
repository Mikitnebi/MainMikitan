using MainMikitan.Domain.Responses.S3Response;

namespace MainMikitan.Domain.Requests.Customer;

public record GetCustomerInfoResponse
{
    public string? FullName { get; set; } = null!;
    public DateOnly? BirthDate { get; set; }
    public int GenderId { get; set; }
    public int NationalityId { get; set; }
    public GetImageResponse? ImageData { get; set; }
}