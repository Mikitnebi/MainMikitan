namespace MainMikitan.Domain.Responses.Rating;

public class GetCustomerRatingResponse
{
    public int CustomerId { get; set; }
    public float Rating { get; set; }
}