using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Domain.Requests.RestaurantRequests {
    public record RestaurantRegistrationEnvironmentRequest {
            [MaxLength(3)]
            public IEnumerable<int> CousinsTypeIds { get; set; } = null!;
            [MaxLength(3)]
            public IEnumerable<int> MusicsTypeIds { get; set; } = null!;
            [MaxLength(3)]
            public IEnumerable<int> EnvironmentTypeIds { get; set; } = null!;
    }
}
