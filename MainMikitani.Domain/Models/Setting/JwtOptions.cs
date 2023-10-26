using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Domain.Models.Setting;

public class JwtOptions
{
    public string IssuerSecurityKey { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public string AccessTokenLifeTimeInMinutes { get; set; } = null!;
    public string RefreshTokenValidityInDays { get; set; } = null!;
}