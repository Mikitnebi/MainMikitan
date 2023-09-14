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
        public string Email { get; set; }
        public string Otp { get; set; }
    }
}
