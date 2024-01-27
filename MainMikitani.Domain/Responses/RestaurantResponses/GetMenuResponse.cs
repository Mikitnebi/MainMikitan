namespace MainMikitan.Domain.Responses.RestaurantResponses;

public class GetMenuResponse
{
    public int Id { get; set; }
    public int CategoryDishId { get; set; }
    public int? ParentDishId { get; set; }
    public int RestaurantId { get; set; }
    public bool IsVerified { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; }
    public bool HasDifferentPicture { get; set; }
    public string DishPicture { get; set; } = null!;
    public DateTime? UpdateAt { get; set; }
    public DateTime CreateAt { get; set; }
    public int CreateUserId { get; set; }
}