using Microsoft.AspNetCore.Http;

namespace MainMikitan.Domain.Requests;

public record AddDishRequest
{
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public int CategoryDishId { get; set; }
    public int? ParentDishId { get; set; }
    public bool IsVerified { get; set; }
    public int RestaurantId { get; set; }
    public bool HasDifferentPicture { get; set; }
    public IFormFile? DishPhoto { get; set; }
    public DateTime CreatedAt { get; set; }
    public int CreateUserId { get; set; }
}