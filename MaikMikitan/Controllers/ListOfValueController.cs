using MainMikitan.API.Filters;
using MainMikitan.Application.Features.ListOfValue;
using MainMikitan.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace MainMikitan.API.Controllers;
[Authorized(Enums.RoleId.Customer)]
[OutputCache(Duration = 60)]
public class ListOfValueController(IMediator mediator) : MainController(mediator)
{
    [HttpPost("kitchen-types")]
    public async Task<IActionResult> GetKitchenTypes(CancellationToken cancellationToken = default) =>
        !ModelState.IsValid ? BadRequest(ModelState) :
            CheckResponse(await Mediator.Send(new ListOfValueQuery((int)Enums.ListOfValueType.Kitchen),cancellationToken));
    [HttpPost("music-types")]
    public async Task<IActionResult> GetMusicTypes(CancellationToken cancellationToken = default) =>
        !ModelState.IsValid ? BadRequest(ModelState) :
            CheckResponse(await Mediator.Send(new ListOfValueQuery((int)Enums.ListOfValueType.Music),cancellationToken));
        
    [HttpPost("environment-types")]
    public async Task<IActionResult> GetEnvironmentTypes(CancellationToken cancellationToken = default) =>
        !ModelState.IsValid ? BadRequest(ModelState) :
            CheckResponse(await Mediator.Send(new ListOfValueQuery((int)Enums.ListOfValueType.Environment),cancellationToken));
    
    [HttpPost("region-types")]
    public async Task<IActionResult> GetRegionTypes(CancellationToken cancellationToken = default) =>
        !ModelState.IsValid ? BadRequest(ModelState) :
            CheckResponse(await Mediator.Send(new ListOfValueQuery((int)Enums.ListOfValueType.Region),cancellationToken));
    
    [HttpPost("citizenship-types")]
    public async Task<IActionResult> GetNationalityTypes(CancellationToken cancellationToken = default) =>
        !ModelState.IsValid ? BadRequest(ModelState) :
            CheckResponse(await Mediator.Send(new ListOfValueQuery((int)Enums.ListOfValueType.Citizenship),cancellationToken));
    
    [HttpPost("ingredients-types")]
    public async Task<IActionResult> GetIngredientsTypes(CancellationToken cancellationToken = default) =>
        !ModelState.IsValid ? BadRequest(ModelState) :
            CheckResponse(await Mediator.Send(new ListOfValueQuery((int)Enums.ListOfValueType.Ingredients),cancellationToken));
}