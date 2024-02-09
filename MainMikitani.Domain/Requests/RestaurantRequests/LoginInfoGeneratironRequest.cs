using System.ComponentModel.DataAnnotations;

namespace MainMikitan.Domain.Requests.RestaurantRequests
{
    public record LoginInfoGeneratironRequest
    {
        [EmailAddress]
        public string Email { get; set; } = null!;
        public bool SendAsEmail { get; set; } = true;
    }
}
