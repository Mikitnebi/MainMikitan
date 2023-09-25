using MainMikitan.Domain.Models.Common;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Models.Setting;
using MainMikitan.Domain.Requests.Customer.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static MainMikitan.Domain.Enums;

namespace MainMikitan.InternalServiceAdapter.Auth
{
    public class AuthService : IAuthService
    {
        private readonly JwtOptions _jwtOptions;

        public AuthService(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }
        public ResponseModel<AuthTokenResponseModel> CustomerAuth(CustomerAuthRequestModel customerAuthModel)
        {
            var response = new ResponseModel<AuthTokenResponseModel>();
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, customerAuthModel.Id.ToString()),
                new Claim(ClaimTypes.Name, customerAuthModel.EmailAdress),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, RoleId.Customer.ToString())
            };
            var token = GetToken(authClaims);
            response.Result = new AuthTokenResponseModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token)
            };
            return response;
        }


        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var key = _jwtOptions.IssuerSecurityKey;
            var issuer = _jwtOptions.Issuer;
            var audience = _jwtOptions.Audience;
            var lifetime = int.Parse(_jwtOptions.AccessTokenLifeTimeInMinutes);

            var authSingingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                expires: DateTime.Now.AddMinutes(lifetime),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSingingKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
    }
}
