using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Domain.Requests.RestaurantRequests
{
    public record LoginInfoGeneratironRequest
    {
        [EmailAddress]
        public string Email { get; set; } = null!;
        public bool SendAsEmail { get; set; } = true;
    }
}
