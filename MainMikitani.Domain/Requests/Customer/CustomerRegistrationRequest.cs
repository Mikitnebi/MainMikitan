using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Domain.Requests
{
    public record CustomerRegistrationRequest
    {
        public string FullName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? MobileNumber { get; set; }
        public string Password { get; set; } = null!;
        public bool RequiredOptions { get; set; }
    }
}
