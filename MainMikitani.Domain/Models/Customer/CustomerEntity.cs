using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MainMikitan.Domain.Models.Customer;

public record CustomerEntity
{
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string FullName { get; set; } = null!;
    [Required]
    [EmailAddress] 
    public string EmailAddress { get; set; } = null!;
    [Required]
    public bool EmailConfirmation { get; set; }
    public string MobileNumber { get; set; } = null!;
    
    public bool MobileNumberConfirmation { get; set; }
    [Required]
    public string HashPassWord { get; set; } = null!;
    [Required]
    public DateTime CreatedAt { get; set; }
    [Required]
    public int StatusId { get; set; }
}
