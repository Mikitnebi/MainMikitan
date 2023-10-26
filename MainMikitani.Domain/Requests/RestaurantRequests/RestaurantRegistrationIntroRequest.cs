using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Domain.Requests.RestaurantRequests {
    public record RestaurantRegistrationIntroRequest
    {
        public string BusinessNameGeo { get; set; } = null!;
        public string BusinessNameEng { get; set; } = null!;

        [Phone] public string PhoneNumber { get; set; } = null!;

        [EmailAddress] public string EmailAddress { get; set; } = null!;
        public int RegionId { get; set; }
    }
}
