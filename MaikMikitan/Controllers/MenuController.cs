using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace MainMikitan.API.Controllers;

[ApiController]
[Route("[controller]")]
[EnableCors("AllowSpecificOrigin")]
public class MenuController : ControllerBase
{
    private readonly IMediator _mediator;

    public MenuController(IMediator mediator)
    {
        _mediator = mediator;
    }
}