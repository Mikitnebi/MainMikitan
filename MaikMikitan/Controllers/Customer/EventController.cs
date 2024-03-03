using MainMikitan.API.Filters;
using MainMikitan.Application.Features.Customer.Queries;
using MainMikitan.Application.Features.Restaurant.Events.Query;
using MainMikitan.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MainMikitan.API.Controllers;

[Authorized(Enums.RoleId.Customer)]
[Route("Customer-Event")]
public class EventController(IMediator mediator) : MainController(mediator)
{
    [HttpGet("Get/AllEvent")]
    public async Task<IActionResult> GetEventInfo(int page, int size, CancellationToken cancellationToken = default) {
        return !ModelState.IsValid ? BadRequest(ModelState) :
            CheckResponse(await Mediator.Send(new GetCustomerEventQuery(page, size), cancellationToken));
    }
}