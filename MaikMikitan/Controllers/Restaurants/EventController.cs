using MainMikitan.API.Filters;
using MainMikitan.Application.Features.Restaurant.Events.Command;
using MainMikitan.Application.Features.Restaurant.Events.Query;
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
            CheckResponse(await Mediator.Send(new CreateOrUpdateEventCommand(RestaurantId, request, new []{ (int)Enums.RestaurantPermissionId.Event }, UserRole!), cancellationToken));
    }
    
    [HttpPost("CreateOrUpdate/EventDetails")]
    public async Task<IActionResult> CreateOrUpdateDetails(CreateOrUpdateEventDetailsRequest request, CancellationToken cancellationToken = default) {
        return !ModelState.IsValid ? BadRequest(ModelState) :
            CheckResponse(await Mediator.Send(new CreateOrUpdateEventDetailsCommand(request, RestaurantId,  new []{ (int)Enums.RestaurantPermissionId.Event }, UserRole!, cancellationToken), cancellationToken));
    }
    
    [HttpGet("Get/EventInfo")]
    public async Task<IActionResult> GetEventInfo(CancellationToken cancellationToken = default) {
        return !ModelState.IsValid ? BadRequest(ModelState) :
            CheckResponse(await Mediator.Send(new GetEventInfoQuery(RestaurantId), cancellationToken));
    }
}