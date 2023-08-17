using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Domain.Requests.Customer.Auth
{
    public record CustomerAuthRequestModel
    {
        public string EmailAdress { get; set; } = null!;
        public int Id { get; set; }
    }
}
