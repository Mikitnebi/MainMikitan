using System.ComponentModel.DataAnnotations;

namespace MainMikitan.Domain.Requests.GeneralRequests;

public class GeneralRegistrationVerifyOtpRequest
{
    [EmailAddress]
    public string Email { get; set; }
    public string Otp { get; set; }
}
