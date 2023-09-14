using System.ComponentModel.DataAnnotations;

namespace MainMikitan.Domain.Models.Customer;

public record CustomerEntity
{
    public int Id { get; set; }
    public string FullName { get; set; }
    [EmailAddress]
    public string EmailAddress { get; set; }
    public bool EmailConfirmation { get; set; }
    public string MobileNumber { get; set; }
    public bool MobileNumberConfirmation { get; set; }
    public string HashPassWord { get; set; }
    public DateTime CreatedAt { get; set; }
    public int StatusId { get; set; }
}
