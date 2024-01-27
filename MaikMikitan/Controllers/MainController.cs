using MainMikitan.API.Extentions;
using MainMikitan.Domain.Models.Commons;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace MainMikitan.API.Controllers;
[ApiController]
[Route("[controller]")]
[EnableCors("AllowSpecificOrigin")]
public class MainController(IMediator mediator) : ControllerBase
{
    protected readonly IMediator Mediator = mediator;
    protected int UserId => User.GetId();

    protected IActionResult CheckResponse<T> (ResponseModel<T> response) =>
        response.HasError ? BadRequest(response) : Ok(response);
    
}