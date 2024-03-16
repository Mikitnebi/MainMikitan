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
            CheckResponse(await Mediator.Send(new SaveCustomerRatingCommand(RestaurantId, UserId, request, UserRole!, new []{ (int)Enums.RestaurantPermissionId.Rating }), cancellationToken));
    }
    
    [HttpGet("Get/{customerId:int}")]
    public async Task<IActionResult> GetCustomerRating(int customerId, CancellationToken cancellationToken = default)
    {
        return !ModelState.IsValid ? BadRequest(ModelState) :
            CheckResponse(await Mediator.Send(new GetCustomerRatingQuery(customerId, RestaurantId, UserId, UserRole!, new []{ (int)Enums.RestaurantPermissionId.Rating }), cancellationToken));
    }
    
    [HttpGet("GetAllCustomersRatings")]
    public async Task<IActionResult> GetAllCustomersRatings(CancellationToken cancellationToken = default)
    {
        return !ModelState.IsValid ? BadRequest(ModelState) :
            CheckResponse(await Mediator.Send(new GetAllCustomersRatingsQuery(RestaurantId, UserId, UserRole!, new []{ (int)Enums.RestaurantPermissionId.Rating }), cancellationToken));
    }
}