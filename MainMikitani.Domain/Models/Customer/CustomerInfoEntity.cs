using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MainMikitan.Domain.Models.Customer;
public class CustomerInfoEntity
{
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public int CustomerId { get; set; }
    [Required]
    public DateOnly BirthDate { get; set; }
    [Required]
    public int GenderId { get; set; }
    [Required]
    public int NationalityId { get; set; }
    public DateTime? UpdatedAt { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
}
    

