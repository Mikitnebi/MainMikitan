using MainMikitan.API.Filters;
using MainMikitan.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MainMikitan.API.Controllers;

[Authorized(Enums.RoleId.Customer)]
public class FilterController(IMediator mediator) : MainController(mediator)
{
    /*
     [HttpGet("RestaurantByDefault")]
    public async Task<IActionResult> GetRestaurantByDefault(CancellationToken cancellationToken = default)
    {
        return !ModelState.IsValid ? BadRequest(ModelState) :
            CheckResponse(await Mediator.Send(new RestaurantByDefaultByCustomerQuery(UserId), cancellationToken));
    }
    */
}