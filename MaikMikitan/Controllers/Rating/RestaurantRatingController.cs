using MainMikitan.API.Filters;
using MainMikitan.Application.Features.Rating.Commands;
using MainMikitan.Application.Features.Rating.Queries;
using MainMikitan.Domain;
using MainMikitan.Domain.Requests.Rating;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MainMikitan.API.Controllers.Rating;

[Route("[controller]")]
[Authorized(Enums.RoleId.Manager, Enums.RoleId.Staff)]
public class RestaurantRatingController(IMediator mediator) : MainController(mediator)
{
    [HttpPost("Save")]
    public async Task<IActionResult> SaveCustomerRating(SaveCustomerRatingRequest request, CancellationToken cancellationToken = default) {
        return !ModelState.IsValid ? BadRequest(ModelState) :
            CheckResponse(await Mediator.Send(new SaveCustomerRatingCommand(RestaurantId, UserId, request), cancellationToken));
    }
    
    [HttpGet("Get/{restaurantId:int}")]
    public async Task<IActionResult> GetRestaurantRating(int restaurantId, CancellationToken cancellationToken = default)
    {
        return !ModelState.IsValid ? BadRequest(ModelState) :
            CheckResponse(await Mediator.Send(new GetRestaurantRatingQuery(restaurantId), cancellationToken));
    }
    
    [HttpGet("GetAllRestaurantRatings")]
    public async Task<IActionResult> GetAllRestaurantsRatings(CancellationToken cancellationToken = default)
    {
        return !ModelState.IsValid ? BadRequest(ModelState) :
            CheckResponse(await Mediator.Send(new GetAllRestaurantsRatingQuery(), cancellationToken));
    }
}