using MainMikitan.API.Filters;
using MainMikitan.Application.Features.Customer.Commands;
using MainMikitan.Application.Features.Restaurant.Info;
using MainMikitan.Domain;
using MainMikitan.Domain.Requests.RestaurantRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static MainMikitan.Domain.Enums;

namespace MainMikitan.API.Controllers.Restaurants {
    public class RestaurantInfoController(IMediator mediator) : MainController(mediator) {

        [Authorized(Enums.RoleId.Manager, Enums.RoleId.Staff)]
        [HttpPost("UpdateInfo")]
        public async Task<IActionResult> CreateOrUpdateInfo(RestaurantInfoRequest model, CancellationToken cancellationToken = default) {
            return !ModelState.IsValid ? BadRequest(ModelState) :
                CheckResponse(await Mediator.Send(new CreateOrUpdateInfoCommand(model, UserId, RoleId, (int)RestaurantPermissionId.Info), cancellationToken));

        }


    }
}
