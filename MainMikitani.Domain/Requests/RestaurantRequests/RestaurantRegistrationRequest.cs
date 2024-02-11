using System.ComponentModel.DataAnnotations;

namespace MainMikitan.Domain.Requests.RestaurantRequests
{
    public record RestaurantRegistrationRequest
    {
        [EmailAddress]
        public string Email { get; set; } = null!;
        public bool SendAsEmail { get; set; } = true;
        [Required]
        [Length(2,50)]
        public string BusinessNameGeo { get; set; } = null!;

        [Required] [Length(2, 50)] 
        public string BusinessNameEng { get; set; } = null!;
        
        [Required] [Length(2, 50)] 
        public string OfficialEmail { get; set; } = null!;
    }
}
