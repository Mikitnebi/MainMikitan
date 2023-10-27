using MainMikitan.Application.Features.Dish.Add.Commands;
using MainMikitan.Domain.Requests;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace MainMikitan.API.Controllers;

[ApiController]
[Route("[controller]")]
[EnableCors("AllowSpecificOrigin")]
public class DishController : ControllerBase
{
    private readonly IMediator _mediator;

    public DishController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("registration")]
    [EnableCors("AllowSpecificOrigin")]
    public async Task<IActionResult> AddCategoryDish(AddCategoryDishRequest request)
    {
        if (ModelState.IsValid) {
            var response = await _mediator.Send(new AddCategoryDishCommand(request));
            if (response.HasError) return BadRequest(response);
            return Ok(response);
        }
        
        return Ok();
    }
}