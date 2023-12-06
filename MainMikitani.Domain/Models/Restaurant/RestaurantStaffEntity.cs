using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MainMikitan.Domain.Models.Restaurant;

public class RestaurantStaffEntity
{
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string NameGeo { get; set; } = null!;
    [Required]
    public string NameEng { get; set; } = null!;
    [Required]
    public int RoleId { get; set; }
    [Required]
    public string PhoneNumber { get; set; } = null!;
    [Required]
    public string? Email { get; set; }
    [Required]
    public bool IsConfirmed { get; set; }
    [Required]
    public string UsernameHash { get; set; } = null!;
    [Required]
    public string PasswordHash { get; set; } = null!;
    [Required]
    public int RestaurantId { get; set; }
    [Required]
    public bool IsActive { get; set; }
    [Required]
    public int UpdateUserId { get; set; }
    public DateTime? UpdateAt { get; set; }
    [Required]
    public int CreateUserId { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }

}