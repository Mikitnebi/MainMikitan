namespace MainMikitan.Domain.Requests;

public record DeactiveDishRequest
{
    public int DishId { get; set; }
    public bool IsActiveStatus { get; set; }
    public int UpdateUserId { get; set; }
}