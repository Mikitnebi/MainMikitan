using MainMikitan.API.Filters;
using MainMikitan.Application.Features.Restaurant.Get;
using MainMikitan.Domain;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace MainMikitan.API.Controllers;

[ApiController]
[Authorized(Enums.RoleId.Manager)]
[Route("[controller]")]
[EnableCors("AllowSpecificOrigin")]
public class MenuController(IMediator mediator) : MainController(mediator)
{

    [HttpGet("get-menu")]
    public async Task<IActionResult> GetMenu()
    {
        return !ModelState.IsValid
            ? BadRequest(ModelState)
            : CheckResponse(await Mediator.Send(new GetMenuCommand(RestaurantId, UserId, UserRole!, new []{ (int)Enums.RestaurantPermissionId.Menu })));
    }
}