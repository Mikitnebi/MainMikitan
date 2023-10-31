namespace MainMikitan.Domain.Requests;

public record DeleteDishRequest
{
    public int DishId { get; set; }
    public bool IsDeletedStatus { get; set; }
}