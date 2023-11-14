using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Domain.Requests.GeneralRequests
{
    public class GeneralRegistrationVerifyOtpRequest
    {
        [EmailAddress] 
        public string Email { get; init; } = null!;
        public string Otp { get; init; } = null!;
    }
}
