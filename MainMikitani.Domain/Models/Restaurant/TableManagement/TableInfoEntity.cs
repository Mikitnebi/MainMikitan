using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MainMikitan.Domain.Models.Restaurant.TableManagement;

public class TableInfoEntity
{
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int TableNumber { get; set; }
    public int MaxPlace { get; set; }
    public int MinPlace { get; set; }
    public int TableEnvironmentListId { get; set; }
    public int TableType { get; set; }
    public decimal XCoordinate { get; set; }
    public decimal YCoordinate { get; set; }
    public DateTime CreatedAt { get; set; }
}