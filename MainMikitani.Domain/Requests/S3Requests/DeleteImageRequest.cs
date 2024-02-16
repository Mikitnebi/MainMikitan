namespace MainMikitan.Domain.Requests.S3Requests;

public record DeleteImageRequest
{
    public string? Key { get; set; }
}
