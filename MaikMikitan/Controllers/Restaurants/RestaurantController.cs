using MainMikitan.API.Filters;
using MainMikitan.Application.Features.Restaurant.Environment.Command;
using MainMikitan.Application.Features.Restaurant.Environment.Query;
using MainMikitan.Application.Features.Restaurant.Info;
using MainMikitan.Application.Features.Restaurant.Info.Query;
using MainMikitan.Application.Features.Restaurant.ParentChild.Command;
using MainMikitan.Application.Features.Restaurant.Registration.Commands;
using MainMikitan.Domain;
using MainMikitan.Domain.Requests.RestaurantRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MainMikitan.API.Controllers.Restaurants
{ 
    [Authorized(Enums.RoleId.Manager, Enums.RoleId.Staff)]
    public class RestaurantController(IMediator mediator) : MainController(mediator)
    {
        [HttpPost("CreateOrUpdate/Info")]
        public async Task<IActionResult> CreateOrUpdateInfo(RestaurantInfoRequest model, CancellationToken cancellationToken = default) {
            return !ModelState.IsValid ? BadRequest(ModelState) :
                CheckResponse(await Mediator.Send(new CreateOrUpdateInfoCommand(model, UserId, UserRole!, RestaurantId,
                    new []{(int)Enums.RestaurantPermissionId.Info}), cancellationToken));

        }
        [Authorized(Enums.RoleId.Manager, Enums.RoleId.Staff)]
        [HttpPost("View/Info")]
        public async Task<IActionResult> ViewInfo(CancellationToken cancellationToken = default) {
            return !ModelState.IsValid ? BadRequest(ModelState) :
                CheckResponse(await Mediator.Send(new ViewInfoQuery(UserId, UserRole!, RestaurantId,
                    new []{(int)Enums.RestaurantPermissionId.Info}), cancellationToken));
        }
        
        [HttpPost("CreateOrUpdate/Environment")]
        public async Task<IActionResult> CreateOrUpdateEnvironment(RestaurantRegistrationEnvironmentRequest model, 
            CancellationToken cancellationToken = default) {
            return !ModelState.IsValid ? BadRequest(ModelState) :
                CheckResponse(await Mediator.Send(new CreateOrUpdateEnvironmentCommand(model, UserId, UserRole!, 
                    RestaurantId, new []{(int)Enums.RestaurantPermissionId.Info}), cancellationToken));

        }
        
        [HttpPost("View/Environment")]
        public async Task<IActionResult> ViewEnvironment(CancellationToken cancellationToken = default) {
            return !ModelState.IsValid ? BadRequest(ModelState) :
                CheckResponse(await Mediator.Send(new ViewEnvironmentQuery( UserId, UserRole!, RestaurantId,
                    new []{(int)Enums.RestaurantPermissionId.Info}), cancellationToken));
        }
        
        [HttpPost("MakeBranch")]
        public async Task<IActionResult> MakeBranchRestaurant()
        {
            return !ModelState.IsValid
                ? BadRequest(ModelState)
                : CheckResponse(await Mediator.Send(new MakeBranchRestaurantCommand(UserId)));
        }
        /*
        [HttpPost("MakeParent")]
        public async Task<IActionResult> MakeParent()
        {
            return !ModelState.IsValid
                ? BadRequest(ModelState)
                : CheckResponse(await Mediator.Send(new MakeParentCommand(UserId)));
        }
*/
    }
}
