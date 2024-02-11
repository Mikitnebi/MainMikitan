using MainMikitan.Application.Features.Restaurant.Registration.Commands;
using MainMikitan.Domain.Requests.Admin;
using MainMikitan.Domain.Requests.RestaurantRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MainMikitan.API.Controllers.Admin;

public class AdminRestaurantController(IMediator mediator) : MainController(mediator)
{
    [HttpPost("LoginInfoGeneration")]
    public async Task<IActionResult> LoginInfoGeneration(
        LoginInfoGenerationRequest request,
        CancellationToken cancellationToken = default)
    {
        return !ModelState.IsValid ? BadRequest(ModelState) 
            : CheckResponse(await Mediator.Send(new LoginInfoGenerationCommand(request.Restaurant, request.Manager), cancellationToken));
    }
}
