namespace MainMikitan.Domain.Requests;

public record VerifyDishRequest
{
    public int DishId { get; set; }
    public bool IsVerifiedStatus { get; set; }
}