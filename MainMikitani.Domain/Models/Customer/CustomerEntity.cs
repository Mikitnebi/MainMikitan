using System.ComponentModel.DataAnnotations;

namespace MainMikitan.Domain.Models.Customer;

public record CustomerEntity
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    [EmailAddress] public string EmailAddress { get; set; } = null!;
    public bool EmailConfirmation { get; set; }
    public string MobileNumber { get; set; } = null!;
    public bool MobileNumberConfirmation { get; set; }
    public string HashPassWord { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public int StatusId { get; set; }
}
