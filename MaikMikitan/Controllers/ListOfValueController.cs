using MainMikitan.API.Filters;
using MainMikitan.Application.Features.ListOfValue;
using MainMikitan.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace MainMikitan.API.Controllers;
[Authorized(Enums.RoleId.Customer)]
public class ListOfValueController(IMediator mediator) : MainController(mediator)
{
    [HttpPost("kitchen-types")]
    [OutputCache(Duration = 60)]
    public async Task<IActionResult> GetKitchenTypes(CancellationToken cancellationToken = default)
    {
        return !ModelState.IsValid ? BadRequest(ModelState) :
            CheckResponse(await Mediator.Send(new ListOfValueQuery((int)Enums.ListOfValueType.Kitchen),cancellationToken));
    }
    [HttpPost("music-types")]
    [OutputCache(Duration = 60)]
    public async Task<IActionResult> GetMusicTypes(CancellationToken cancellationToken = default)
    {
        return !ModelState.IsValid ? BadRequest(ModelState) :
            CheckResponse(await Mediator.Send(new ListOfValueQuery((int)Enums.ListOfValueType.Music),cancellationToken));
    }
    [HttpPost("environment-types")]
    [OutputCache(Duration = 60)]
    public async Task<IActionResult> GetEnvironmentTypes(CancellationToken cancellationToken = default)
    {
        return !ModelState.IsValid ? BadRequest(ModelState) :
            CheckResponse(await Mediator.Send(new ListOfValueQuery((int)Enums.ListOfValueType.Environment),cancellationToken));
    }
}