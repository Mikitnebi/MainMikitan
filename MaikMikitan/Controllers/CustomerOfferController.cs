using MainMikitan.API.Filters;
using MainMikitan.Domain;
using MediatR;

namespace MainMikitan.API.Controllers;

[Authorized(Enums.RoleId.Customer)]
public class CustomerOfferController(IMediator mediator) : MainController(mediator)
{
    
}