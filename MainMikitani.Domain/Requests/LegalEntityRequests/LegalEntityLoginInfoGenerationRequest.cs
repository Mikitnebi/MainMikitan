using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Domain.Requests.LegalEntityRequests
{
    public class LegalEntityLoginInfoGenerationRequest
    {
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [Length(2, 50)]
        public string BusinessNameGeo { get; set; } = null!;

        [Required]
        [Length(2, 50)]
        public string BusinessNameEng { get; set; } = null!;
        [Required]
        [Length(2, 50)]
        public string LegalEntityRepresentativeNameGeo { get; set; } = null;
        [Required]
        [Length(2, 50)]
        public string LegalEntityRepresentativeNameEng { get; set; } = null;
        [Length(5, 20)]
        public string PhoneNumber { get; set; }


    }
}
