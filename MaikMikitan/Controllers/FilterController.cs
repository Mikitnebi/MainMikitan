using MainMikitan.API.Filters;
using MainMikitan.Application.Features.Filter;
using MainMikitan.Domain;
using MainMikitan.Domain.Requests.Filter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MainMikitan.API.Controllers;

[Authorized(Enums.RoleId.Customer)]
public class FilterController(IMediator mediator) : MainController(mediator)
{
    [HttpGet("RestaurantByDefault")]
    public async Task<IActionResult> GetRestaurantByDefault(int page, int size, CancellationToken cancellationToken = default)
    {
        return !ModelState.IsValid ? BadRequest(ModelState) :
            CheckResponse(await Mediator.Send(new RestaurantByDefaultByCustomerQuery(UserId, IpAddress, page, size), cancellationToken));
    }
    
    [HttpGet("RestaurantByFilter")]
    public async Task<IActionResult> GetRestaurantByFilter(FilterRequestModel request, int page, int size, CancellationToken cancellationToken = default)
    {
        return !ModelState.IsValid ? BadRequest(ModelState) :
            CheckResponse(await Mediator.Send(new RestaurantByFilterByCustomerQuery(UserId, IpAddress, page, size, request), cancellationToken));
    }
}