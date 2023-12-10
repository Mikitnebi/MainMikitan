using System.ComponentModel.DataAnnotations;

namespace MainMikitan.Domain.Models.Common;

public class CategoryEntity
{
    [Required]
    public int Id { get; set; }
    [Required]
    public int StatusId { get; set; }
    [Required]
    public string NameGeo { get; set; } = null!;
    [Required]
    public string NameEng { get; set; } = null!;
    [Required]
    public DateTime CreatedAt { get; set; }
}