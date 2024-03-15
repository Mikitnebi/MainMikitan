using MainMikitan.API.Filters;
using MainMikitan.Domain;
using MainMikitan.Domain.Requests.Customer.Feature.Reservation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MainMikitan.API.Controllers;

[Authorized(Enums.RoleId.Customer)]
public class CustomerReservation(IMediator mediator) : MainController(mediator)
{
    /*public async Task<IActionResult> Reserve(ReservationRequest request, CancellationToken cancellationToken = default)
    {
        /*return !ModelState.IsValid
            ? BadRequest(ModelState)
            : CheckResponse(await Mediator.Send(await ReserveCommand(request), cancellationToken));#1#
        return null;
    }*/
}