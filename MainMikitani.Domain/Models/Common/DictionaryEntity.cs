using System.ComponentModel.DataAnnotations;

namespace MainMikitan.Domain.Models.Common;

public class DictionaryEntity
{
    [Required]
    public int Id { get; set; }
    [Required]
    public int SectorId { get; set; }
    [Required]
    public string GeoName { get; set; } = null!;
    [Required]
    public string EngName { get; set; } = null!;
}