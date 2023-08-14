using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Domain.Requests.Customer
{
    public record CustomerLoginRequest
    { 
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
