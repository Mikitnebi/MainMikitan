using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MainMikitan.Domain.Models.Restaurant;

public class RestaurantStaffEntity
{
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [Length(5, 50)]
    public string FullNameGeo { get; set; } = null!;
    [Required]
    [Length(5, 50)]
    public string FullNameEng { get; set; } = null!;
    [Required]
    public bool IsManager { get; set; }
    [Required]
    [Length(5, 20)]
    public string PhoneNumber { get; set; } = null!;
    [Required]
    public bool PhoneConfirmation { get; set; }
    [Length(5, 50)]
    public string? Email { get; set; }
    private bool? EmailConfirmation { get; set; }
    [Required]
    public string UserNameHash { get; set; } = null!;
    [Required]
    public string PasswordHash { get; set; } = null!;
    [Required]
    public int RestaurantId { get; set; }
    [Required]
    public bool IsActive { get; set; }
    public int? UpdateUserId { get; set; }
    public DateTime? UpdateAt { get; set; }
    public int? CreateUserId { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }

}