using MainMikitan.Domain.Models.Common;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Models.Setting;
using MainMikitan.Domain.Requests.Customer.Auth;
using MainMikitan.Domain.Requests.RestaurantRequests.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Primitives;
using static MainMikitan.Domain.Enums;

namespace MainMikitan.InternalServiceAdapter.Auth
{
    public class AuthService(IOptions<JwtOptions> jwtOptions) : IAuthService
    {
        private readonly JwtOptions _jwtOptions = jwtOptions.Value;

        public AuthTokenResponseModel CustomerAuth(CustomerAuthRequestModel customerAuthModel)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, customerAuthModel.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, RoleId.Customer.ToString())
            };
            var token = GetToken(authClaims);
            return new AuthTokenResponseModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        public AuthTokenResponseModel StaffAuth(StaffAuthModel staffAuthModel, List<int>? permissions = null) {
            var response = new ResponseModel<AuthTokenResponseModel>();
            var authClaims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, staffAuthModel.StaffId.ToString()),
                new (ClaimTypes.GivenName, staffAuthModel.RestaurantId.ToString()),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new (ClaimTypes.Role, !staffAuthModel.IsManager ? RoleId.Staff.ToString() : RoleId.Manager.ToString())
            };
            if (!staffAuthModel.IsManager)
            {
                var permissionString = new StringBuilder("|");
                foreach (var permissionId in permissions!)
                {
                    permissionString.Append(permissionId);
                    permissionString.Append('|');
                }
                authClaims.Add(new Claim(ClaimTypes.Authentication, permissionString.ToString()));
            }

            var token = GetToken(authClaims);
            return new AuthTokenResponseModel {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        private JwtSecurityToken GetToken(IEnumerable<Claim> authClaims)
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
