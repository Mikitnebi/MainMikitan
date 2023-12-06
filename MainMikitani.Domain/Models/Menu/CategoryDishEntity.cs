using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MainMikitan.Domain.Models.Menu;

public class CategoryDishEntity
{
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string NameGeo { get; set; } = null!;
    [Required]
    public string NameEng { get; set; } = null!;
    public DateTime? UpdatedAt { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
}