using System.ComponentModel.DataAnnotations;

namespace MainMikitan.Domain.Requests;

public class CustomerRegistrationRequest
{
    [MaxLength(40, ErrorMessage = ErrorResponseType.BadTypeFullName)]
    [MinLength(1, ErrorMessage = ErrorResponseType.BadTypeFullName)]
    public string FirstName { get; set; } = null!;
    [MaxLength(40, ErrorMessage = ErrorResponseType.BadTypeFullName)]
    [MinLength(1, ErrorMessage = ErrorResponseType.BadTypeFullName)]

    public string LastName { get; set; } = null!;
    public bool AdultnessConfirmation { get; set; }
    [EmailAddress(ErrorMessage = ErrorResponseType.BadTypeEmailAddress)]
    [MaxLength(40, ErrorMessage = ErrorResponseType.BadTypeEmailAddress)]
    [MinLength(4, ErrorMessage = ErrorResponseType.BadTypeEmailAddress)]
    public string? Email { get; set; } = null!;
    public string? MobileNumber { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool RequiredOptions { get; set; }
}
