using MainMikitan.API.Filters;
using MainMikitan.Application.Features.Table.Command.Add;
using MainMikitan.Application.Features.Table.Command.Delete;
using MainMikitan.Domain;
using MainMikitan.Domain.Requests.TableRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace MainMikitan.API.Controllers;

public class TableController(IMediator mediator) : MainController(mediator)
{
    [Authorized(Enums.RoleId.Manager)]
    [HttpPost("add-table")]
    public async Task<IActionResult> AddTable(List<AddTableRequest> request)
    {
        return !ModelState.IsValid
            ? BadRequest(ModelState)
            : CheckResponse(await Mediator.Send(new AddTableCommand(request, RestaurantId)));
    }
    
    [Authorized(Enums.RoleId.Manager)]
    [HttpPost("delete-table")]
    public async Task<IActionResult> DeleteTable(DeleteTableRequest request)
    {
        return !ModelState.IsValid
            ? BadRequest(ModelState)
            : CheckResponse(await Mediator.Send(new DeleteTableCommand(request)));
    }
}