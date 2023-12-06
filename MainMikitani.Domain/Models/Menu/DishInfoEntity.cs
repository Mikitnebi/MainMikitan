using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MainMikitan.Domain.Models.Menu;

public class DishInfoEntity
{
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public int DishId { get; set; }
    [Required]
    public string NameGeo { get; set; } = null!;
    [Required]
    public string NameEng { get; set; } = null!;
    [Required]
    public string IngredientsGeo { get; set; } = null!;
    [Required]
    public string IngredientsEng { get; set; } = null!;
    public string? DescriptionGeo { get; set; } 
    public string? DescriptionEng { get; set; }
    [Required]
    public DateTime CreateAt { get; set; }
}