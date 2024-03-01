using MainMikitan.API.Filters;
using MainMikitan.Application.Features.Restaurant.ParentChild.Command;
using MainMikitan.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MainMikitan.API.Controllers.Restaurants
{
    [Authorized(Enums.RoleId.Manager)]
    public class RestaurantController(IMediator mediator) : MainController(mediator)
    {
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
