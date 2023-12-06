using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MainMikitan.Domain.Models.Menu;

public class DishEntity
{
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public int CategoryDishId { get; set; }
    public int? ParentDishId { get; set; }
    [Required]
    public int RestaurantId { get; set; }
    [Required]
    public bool IsVerified { get; set; }
    [Required]
    public bool IsDeleted { get; set; }
    [Required]
    public bool IsActive { get; set; }
    [Required]
    public bool HasDifferentPicture { get; set; }
    public DateTime? UpdateAt { get; set; }
    public int? UpdateUserId { get; set; }
    [Required]
    public DateTime CreateAt { get; set; }
    [Required]
    public int CreateUserId { get; set; }
}