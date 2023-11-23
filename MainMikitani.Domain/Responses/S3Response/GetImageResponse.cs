namespace MainMikitan.Domain.Responses.S3Response;

public record GetImageResponse(string Url, int? DishId = null);
