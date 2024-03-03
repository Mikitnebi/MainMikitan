using MainMikitan.API.Filters;
using MainMikitan.Application.Features.Comment.Commands;
using MainMikitan.Domain;
using MainMikitan.Domain.Requests.Comment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MainMikitan.API.Controllers.Comment;

[Route("[controller]")]
[Authorized(Enums.RoleId.Customer)]
public class RestaurantCommentController(IMediator mediator) : MainController(mediator)
{
    [HttpPost("Save")]
    public async Task<IActionResult> GetEventInfo(CreateRestaurantCommentRequest request, CancellationToken cancellationToken = default) {
        return !ModelState.IsValid ? BadRequest(ModelState) :
            CheckResponse(await Mediator.Send(new SaveRestaurantCommentCommand(request, UserId), cancellationToken));
    }
}