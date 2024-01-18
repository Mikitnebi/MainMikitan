namespace MainMikitan.Domain.Requests;

public record UpdateDishStatusRequest
{
    public int DishId { get; set; }
    public bool IsActiveStatus { get; set; }
}