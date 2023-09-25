using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Security.Claims;
using static MainMikitan.Domain.Enums;

namespace MainMikitan.API.Extentions
{
    public static class ClaimsPrincipalExtentions
    {
        #region RestaurantClaims
        public static int GetRestaurantId(this ClaimsPrincipal claimsPrincipal) 
        {
            var restaurantId = 0;
            var role = claimsPrincipal.FindFirstValue(ClaimTypes.Role);
            if( role == RoleId.Restaurant.ToString())
            {
                int.TryParse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier), out restaurantId);
            }
            return restaurantId;
        }
        public static string GetRestaurantEmail(this ClaimsPrincipal claimsPrincipal)
        {
            string customerEmail = string.Empty;
            var role = claimsPrincipal.FindFirstValue(ClaimTypes.Role);
            if (role == RoleId.Restaurant.ToString())
            {
                customerEmail = claimsPrincipal.FindFirstValue(ClaimTypes.Name);
            }
            return customerEmail;
        }
        #endregion
        #region Customer
        public static int GetCustomerId(this ClaimsPrincipal claimsPrincipal)
        {
            var customerId = 0;
            var role = claimsPrincipal.FindFirstValue(ClaimTypes.Role);
            if (role == RoleId.Customer.ToString())
            {
                int.TryParse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier), out customerId);
            }
            return customerId;
        }
        public static string GetCustomerEmail(this ClaimsPrincipal claimsPrincipal)
        {
            string customerEmail = string.Empty;
            var role = claimsPrincipal.FindFirstValue(ClaimTypes.Role);
            if (role == RoleId.Customer.ToString())
            {
                customerEmail = claimsPrincipal.FindFirstValue(ClaimTypes.Name);
            }
            return customerEmail;
        }
        #endregion

    }
}
