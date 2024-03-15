// using Microsoft.AspNetCore.Http;

// using Microsoft.AspNetCore.Http;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MainMikitan.Application.Services.CacheServices.RestaurantCacheService;
using Microsoft.IdentityModel.Tokens;

namespace MainMikitan.API.Middleware;

public class SubscriptionControlMiddleware(RequestDelegate next)
{
    
    public async Task InvokeAsync(HttpContext context, IServiceScopeFactory serviceScopeFactory)
    {
        // var controller = context.GetRouteData().Values.ElementAt(1).Value;
        // string[] restaurantControllers =
        // [
        //     "Dish",
        //     "Event",
        //     "Menu",
        //     "Restaurant",
        //     "Table",
        //     "RestaurantRating",
        //     "RestaurantComment",
        //     "AdminRestaurant"
        // ];
        // if (!restaurantControllers.Contains(controller)) throw new Exception("No Permission");
        //
        // var scope = serviceScopeFactory.CreateScope();
        // var restaurantCache = scope.ServiceProvider.GetService<IRestaurantCacheService>();
        //
        // var action = context.GetRouteData().Values.ElementAt(0).Value;
        //
        // var token = context.Request.Headers.Authorization.ToString()["bearer ".Length..];
        // var jwtToken = new JwtSecurityToken(token);
        // var userId = jwtToken.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
        // var restaurantIdData = jwtToken.Claims.First(claim => claim.Type == ClaimTypes.GivenName).Value;
        // if (!int.TryParse(restaurantIdData, out var restaurantId)) throw new Exception("Incorrect Id");
        // var data = await restaurantCache!.GetRestaurantSubscriptionTypes(restaurantId);
        //
        // Console.WriteLine($"Request targeting endpoint: dsadas");
    }
}