using MainMikitan.API.Filters;
using MainMikitan.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MainMikitan.API.Controllers;

[Authorized(Enums.RoleId.Customer)]
public class CustomerReservation(IMediator mediator) : MainController(mediator)
{
}