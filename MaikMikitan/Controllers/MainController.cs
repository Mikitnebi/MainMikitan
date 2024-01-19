using MainMikitan.API.Extentions;
using MainMikitan.Domain.Models.Commons;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace MainMikitan.API.Controllers;
[ApiController]
[Route("[controller]")]
[EnableCors("AllowSpecificOrigin")]
public class MainController : ControllerBase
{
    protected readonly IMediator Mediator;
    protected int UserId => User.GetId();

    public MainController(IMediator mediator) =>
        Mediator = mediator;

    protected IActionResult CheckResponse<T> (ResponseModel<T> response) =>
        response.HasError ? BadRequest(response) : Ok(response);
    
}