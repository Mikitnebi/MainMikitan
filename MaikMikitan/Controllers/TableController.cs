using MainMikitan.API.Filters;
using MainMikitan.Application.Features.Table.Command.Add;
using MainMikitan.Domain;
using MainMikitan.Domain.Requests.TableRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace MainMikitan.API.Controllers;

public class TableController(IMediator mediator) : MainController(mediator)
{
    [Authorized(Enums.RoleId.Manager)]
    [HttpGet("add-table")]
    public async Task<IActionResult> AddTable(AddTableRequest request)
    {
        return !ModelState.IsValid
            ? BadRequest(ModelState)
            : CheckResponse(await Mediator.Send(new AddTableCommand(request, UserId)));
    }
}