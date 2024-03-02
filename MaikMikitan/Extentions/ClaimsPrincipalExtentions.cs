using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Security.Claims;
using static MainMikitan.Domain.Enums;

namespace MainMikitan.API.Extentions
{
    public static class ClaimsPrincipalExtentions
    {
        public static int GetId(this ClaimsPrincipal claimsPrincipal)
        {
            int.TryParse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier), out var id);
            return id;
        }
        public static string? GetRole(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(ClaimTypes.Role);
        }
        public static int GetRestaurantId(this ClaimsPrincipal claimsPrincipal) {
            int.TryParse(claimsPrincipal.FindFirstValue(ClaimTypes.GivenName), out var id);
            return id;
        }

    }
}
