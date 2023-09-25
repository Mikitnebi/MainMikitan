using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using static MainMikitan.Domain.Enums;

namespace MainMikitan.API.Filters
{
    public class AuthorizedAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string _roleArray;

        public AuthorizedAttribute(RoleId role)
        {
            _roleArray = $"{ role },";
        }
        public AuthorizedAttribute(RoleId role1, RoleId role2)
        {
            _roleArray = $"{role1},{role2},";
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var claims = context.HttpContext.User.Claims;
            if(!string.IsNullOrEmpty(_roleArray) )
            {
                if (!claims.Any(x => x.Type == ClaimTypes.Role && _roleArray.Contains(x.Value)))
                    context.Result = new UnauthorizedResult();
            }
            return;
        }
    }
}
