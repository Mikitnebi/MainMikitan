namespace MainMikitan.Domain.Models.Restaurant;

public class RestaurantStaff
{
    public int Id { get; set; }
    public string NameGeo { get; set; } = null!;
    public string NameEng { get; set; } = null!;
    public int RoleId { get; set; }
    public string PhoneNumber { get; set; } = null!;
    public string? Email { get; set; }
    public bool IsConfirmed { get; set; }
    public string UsernameHash { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public int RestaurantId { get; set; }
    public bool IsActive { get; set; }
    public int UpdateUserId { get; set; }
    public DateTime? UpdateAt { get; set; }
    public int CreateUserId { get; set; }
    public DateTime CreatedAt { get; set; }

}