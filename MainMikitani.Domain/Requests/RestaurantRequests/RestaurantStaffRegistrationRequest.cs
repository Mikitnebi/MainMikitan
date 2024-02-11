using System.ComponentModel.DataAnnotations;

namespace MainMikitan.Domain.Requests.RestaurantRequests;

public record RestaurantStaffRegistrationRequest
{
    [Required]
    [Length(5, 50)]
    public string FullNameGeo { get; set; } = null!;
    [Required]
    [Length(5, 50)]
    public string FullNameEng { get; set; } = null!;
    [Required]
    public bool IsManager { get; set; }
    [Required]
    [Length(5, 20)]
    public string PhoneNumber { get; set; } = null!;
    [Length(5, 50)]
    public string? Email { get; set; }
}