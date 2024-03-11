using MainMikitan.Application.Features.LegalEntity.Commands;
using MainMikitan.Application.Features.Restaurant.Registration.Commands;
using MainMikitan.Domain.Requests.LegalEntityRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MainMikitan.API.Controllers.Admin
{
    public class AdminLEController(IMediator mediator) : MainController(mediator)
    {

        [HttpPost("LegalEntity/LoginInfoGeneration")]
        public async Task<IActionResult> LoginInfoGeneration(
            LegalEntityLoginInfoGenerationRequest request,
            CancellationToken cancellationToken = default) =>
             !ModelState.IsValid ? BadRequest(ModelState)
                : CheckResponse(await Mediator.Send(new LELoginInfoGenerationCommand(request), cancellationToken));
    }
}
