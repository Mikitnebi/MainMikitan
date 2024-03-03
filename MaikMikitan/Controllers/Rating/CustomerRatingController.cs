using MainMikitan.API.Filters;
using MainMikitan.Application.Features.Rating.Commands;
using MainMikitan.Application.Features.Rating.Queries;
using MainMikitan.Domain;
using MainMikitan.Domain.Requests.Rating;
using MainMikitan.Domain.Responses.Rating;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MainMikitan.API.Controllers.Rating;

[Route("[controller]")]
[Authorized(Enums.RoleId.Customer)]
public class CustomerRatingController(IMediator mediator) : MainController(mediator)
{
    [HttpPost("Save")]
    public async Task<IActionResult> GetEventInfo(SaveRestaurantRatingRequest request, CancellationToken cancellationToken = default) {
        return !ModelState.IsValid ? BadRequest(ModelState) :
            CheckResponse(await Mediator.Send(new SaveRestaurantRatingCommand(UserId, request), cancellationToken));
    }

    [HttpGet("Get/{customerId:int}")]
    public async Task<IActionResult> GetCustomerRating(int customerId, CancellationToken cancellationToken = default)
    {
        return !ModelState.IsValid ? BadRequest(ModelState) :
            CheckResponse(await Mediator.Send(new GetCustomerRatingQuery(customerId), cancellationToken));
    }
    
    [HttpGet("GetAllCustomersRatings")]
    public async Task<IActionResult> GetAllCustomersRatings(CancellationToken cancellationToken = default)
    {
        return !ModelState.IsValid ? BadRequest(ModelState) :
            CheckResponse(await Mediator.Send(new GetAllCustomersRatingsQuery(), cancellationToken));
    }
}