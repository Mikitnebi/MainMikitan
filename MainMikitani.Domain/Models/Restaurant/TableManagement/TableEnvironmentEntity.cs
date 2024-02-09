using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MainMikitan.Domain.Models.Restaurant.TableManagement;

public class TableEnvironmentEntity
{
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int TableId { get; set; }
    public int EnvironmentId { get; set; }
    public DateTime CreatedAt { get; set; }
}