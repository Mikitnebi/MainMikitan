using MainMikitan.API.Filters;
using MainMikitan.Domain;
using MediatR;

namespace MainMikitan.API.Controllers;

[Authorized(Enums.RoleId.Customer)]
public class FilterController(IMediator mediator) : MainController(mediator)
{
    
}