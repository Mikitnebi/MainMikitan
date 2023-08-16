using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Domain.Models.Setting
{
    public class JwtOptions
    {
        public string IssuerSecurityKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string AccessTokenLifeTimeInMinutes { get; set; }
        public string RefreshTokenValidityInDays { get; set; }
    }
}
