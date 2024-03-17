using System.Text;
using MainMikitan.Database.Features.Restaurant.Interface;
using Newtonsoft.Json;

namespace MainMikitan.API.Middleware;

public class GlobalExceptionHandlerMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
{
    
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            var scope = serviceScopeFactory.CreateScope();
            var logger = scope.ServiceProvider.GetService<ILoggerCommandRepository>();
            var request = string.Empty;
            var response = string.Empty;
            try
            {
                request = JsonConvert.SerializeObject(httpContext.Request);
                response = JsonConvert.SerializeObject(httpContext.Response);
            }
            catch (Exception e)
            {
                // ignored
            }

            await logger!.AddLogInDb(ex, ex.Source!, request, response);
            var error = new { message = ex.Message };
            var errorJson = JsonConvert.SerializeObject(error);
            httpContext.Response.StatusCode = httpContext.Response.StatusCode;
            httpContext.Response.ContentType = httpContext.Response.ContentType;
            await httpContext.Response.WriteAsync(errorJson, Encoding.UTF8);
        }
    }
}