using System.ComponentModel.DataAnnotations;

namespace MainMikitan.Domain.Models.Common;

public class ChangeLogEntity
{
    [Required]
    public int Id { get; set; }
    [Required] 
    public string OldValues { get; set; } = null!;
    [Required]
    public string NewValues { get; set; } = null!;
    [Required]
    public int StatusId { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
}