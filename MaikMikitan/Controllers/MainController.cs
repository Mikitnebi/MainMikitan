using System.Net;
using MainMikitan.API.Extentions;
using MainMikitan.Domain.Models.Commons;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace MainMikitan.API.Controllers;
[ApiController]
[Route("[controller]")]
[EnableCors("AllowSpecificOrigin")]
public class MainController(IMediator mediator) : ControllerBase
{
    protected readonly IMediator Mediator = mediator;
    protected int UserId => User.GetId();
    protected int RoleId => User.GetRoleId();

    protected string IpAddress
    {
        get
        {
            string ipAddress;
            var httpContext = Request?.HttpContext;
            if (!string.IsNullOrEmpty(httpContext?.Request.Headers["CF-CONNECTING-IP"]))
                ipAddress = httpContext?.Request.Headers["CF-CONNECTING-IP"]!;
            else
                ipAddress = !string.IsNullOrEmpty(Request?.HttpContext?.GetServerVariable("HTTP_X_FORWARDED_FOR")) 
                    ? httpContext?.GetServerVariable("HTTP_X_FORWARDED_FOR")! : httpContext?.GetServerVariable("REMOTE_ADDR")!;

            if (ipAddress.Contains(','))
                ipAddress = ipAddress.Split(',').First().Trim();
            return IPAddress.Parse(ipAddress).MapToIPv4().ToString();
        }
    }

    protected IActionResult CheckResponse<T> (ResponseModel<T> response) =>
        response.HasError ? BadRequest(response) : Ok(response);
    
}