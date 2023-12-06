using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MainMikitan.Domain.Models.Restaurant;

public class RestaurantEnvironmentInfoEntity
{
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public int RestaurantId { get; set; }
    [Required]
    public int EnvironmentId { get; set; }
    [Required]
    public bool IsActive { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
}