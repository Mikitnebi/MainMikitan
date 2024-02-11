using MainMikitan.API.Extentions;
using MainMikitan.API.Filters;
using MainMikitan.Application.Features.Customer.Commands;
using MainMikitan.Application.Features.Restaurant.ParentChild.Command;
using MainMikitan.Application.Features.Restaurant.Registration.Commands;
using MainMikitan.Domain;
using MainMikitan.Domain.Requests.GeneralRequests;
using MainMikitan.Domain.Requests.RestaurantRequests;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using static MainMikitan.Domain.Enums;

namespace MainMikitan.API.Controllers.Restaurants
{
    [Authorized(Enums.RoleId.Manager)]
    public class RestaurantController(IMediator mediator) : MainController(mediator)
    {
        #region Registration
        

        [HttpPost("registration/StarterInfo")]
        [Authorized(Enums.RoleId.Manager)]
        public async Task<IActionResult> RestaurantInfo(RestaurantInfoRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var response = await Mediator.Send(new RestaurantInfoUpdateCommand(request, User.GetId()));
            if (response.HasError) return BadRequest(response);
            return Ok(response);
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
        #endregion
    }
}
