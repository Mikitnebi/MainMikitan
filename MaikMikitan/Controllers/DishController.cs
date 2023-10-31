using MainMikitan.Application.Features.Dish.Add.Commands;
using MainMikitan.Application.Features.Dish.Delete.Commands;
using MainMikitan.Application.Features.Dish.Get.Commands;
using MainMikitan.Application.Features.Dish.Update.Commands;
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
    [Route("add-dish-category")]
    [EnableCors("AllowSpecificOrigin")]
    public async Task<IActionResult> AddCategoryDish(List<AddCategoryDishRequest> request)
    {
        if (ModelState.IsValid) {
            var response = await _mediator.Send(new AddCategoryDishCommand(request));
            if (response.HasError) return BadRequest(response);
            return Ok(response);
        }
        
        return Ok();
    }
    
    [HttpPost]
    [Route("add-dish")]
    [EnableCors("AllowSpecificOrigin")]
    public async Task<IActionResult> AddDish(List<AddDishRequest> request)
    {
        if (ModelState.IsValid) {
            var response = await _mediator.Send(new AddDishCommand(request));
            if (response.HasError) return BadRequest(response);
            return Ok(response);
        }
        
        return Ok();
    }
    
    [HttpPost]
    [Route("add-dish-info")]
    [EnableCors("AllowSpecificOrigin")]
    public async Task<IActionResult> AddDishInfo(List<AddDishInfoRequest> request)
    {
        if (ModelState.IsValid) {
            var response = await _mediator.Send(new AddDishInfoCommand(request));
            if (response.HasError) return BadRequest(response);
            return Ok(response);
        }
        
        return Ok();
    }
    
    [HttpPost]
    [Route("delete-dish")]
    [EnableCors("AllowSpecificOrigin")]
    public async Task<IActionResult> DeleteDish(DeleteDishRequest request)
    {
        if (ModelState.IsValid) {
            var response = await _mediator.Send(new DeleteDishCommand(request));
            if (response.HasError) return BadRequest(response);
            return Ok(response);
        }
        
        return Ok();
    }
    
    [HttpPost]
    [Route("deactive-dish")]
    [EnableCors("AllowSpecificOrigin")]
    public async Task<IActionResult> DeactiveDish(DeactiveDishRequest request)
    {
        if (ModelState.IsValid) {
            var response = await _mediator.Send(new DeactiveDishCommand(request));
            if (response.HasError) return BadRequest(response);
            return Ok(response);
        }
        
        return Ok();
    }
    
    [HttpPost]
    [Route("update-dish-info")]
    [EnableCors("AllowSpecificOrigin")]
    public async Task<IActionResult> UpdateDishInfo(UpdateDishInfoRequest request)
    {
        if (ModelState.IsValid) {
            var response = await _mediator.Send(new UpdateDishInfoCommand(request));
            if (response.HasError) return BadRequest(response);
            return Ok(response);
        }
        
        return Ok();
    }
    
    [HttpPost]
    [Route("verify-dish")]
    [EnableCors("AllowSpecificOrigin")]
    public async Task<IActionResult> VerifyDish(VerifyDishRequest request)
    {
        if (ModelState.IsValid) {
            var response = await _mediator.Send(new VerifyDishCommand(request));
            if (response.HasError) return BadRequest(response);
            return Ok(response);
        }
        
        return Ok();
    }
    
    [HttpGet]
    [Route("get-restaurant-menu")]
    [EnableCors("AllowSpecificOrigin")]
    public async Task<IActionResult> GetRestaurantMenu(GetAllDishesRequest request)
    {
        if (ModelState.IsValid) {
            var response = await _mediator.Send(new GetAllDishesCommand(request));
            if (response.HasError) return BadRequest(response);
            return Ok(response);
        }
        
        return Ok();
    }
}