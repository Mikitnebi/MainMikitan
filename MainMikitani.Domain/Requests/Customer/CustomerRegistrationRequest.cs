using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Domain.Requests
{
    public class CustomerRegistrationRequest
    {
        [MaxLength(40, ErrorMessage = ErrorType.BadTypeFullName)]
        [MinLength(4, ErrorMessage = ErrorType.BadTypeFullName)]
        public string FullName { get; set; } = null!;
        public bool AdultnessConfirmation { get; set; }
        [EmailAddress(ErrorMessage = ErrorType.BadTypeEmailAddress)]
        [MaxLength(40, ErrorMessage = ErrorType.BadTypeEmailAddress)]
        [MinLength(4, ErrorMessage = ErrorType.BadTypeEmailAddress)]
        public string? Email { get; set; } = null!;
        public string? MobileNumber { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool RequiredOptions { get; set; }
    }
}
