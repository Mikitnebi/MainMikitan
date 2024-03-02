using MainMikitan.API.Filters;
using MainMikitan.Application.Features.Restaurant.Events.Command;
using MainMikitan.Domain;
using MainMikitan.Domain.Requests.RestaurantRequests.Event;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MainMikitan.API.Controllers.Restaurants;

[Authorized(Enums.RoleId.Manager)]
[Route("Event")]
public class EventController(IMediator mediator) : MainController(mediator)
{
    [HttpPost("CreateOrUpdate/Event")]
    public async Task<IActionResult> CreateOrUpdateInfo(CreateOrUpdateEventRequest request, CancellationToken cancellationToken = default) {
        return !ModelState.IsValid ? BadRequest(ModelState) :
            CheckResponse(await Mediator.Send(new CreateOrUpdateEventCommand(UserId, request, cancellationToken), cancellationToken));
    }
}