namespace MainMikitan.Domain.Requests;

public class GetAllDishesForCustomerRequest
{
    public int RestaurantId { get; set; }
    public int? CategoryId { get; set; }
}