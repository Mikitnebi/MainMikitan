using MainMikitan.API.Filters;
using MainMikitan.Application.Features.Dish.Add.Commands;
using MainMikitan.Application.Features.Dish.Delete.Commands;
using MainMikitan.Application.Features.Dish.Get.Commands;
using MainMikitan.Application.Features.Dish.Update.Commands;
using MainMikitan.Domain;
using MainMikitan.Domain.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MainMikitan.API.Controllers;

[Authorized(Enums.RoleId.Restaurant)]
public class DishController : MainController
{
    public DishController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    [Route("add-dish-category")]
    public async Task<IActionResult> AddCategoryDish(List<AddCategoryDishRequest> request)
    {
        if (ModelState.IsValid) {
            var response = await Mediator.Send(new AddCategoryDishCommand(request));
            if (response.HasError) return BadRequest(response);
            return Ok(response);
        }
        
        return Ok();
    }
    
    [HttpPost]
    [Route("add-dish")]
    public async Task<IActionResult> AddDish(List<AddDishRequest> request)
    {
        if (ModelState.IsValid) {
            var response = await Mediator.Send(new AddDishCommand(request, UserId));
            if (response.HasError) return BadRequest(response);
            return Ok(response);
        }
        
        return Ok();
    }
    
    [HttpPost]
    [Route("add-dish-info")]
    public async Task<IActionResult> AddDishInfo(List<AddDishInfoRequest> request)
    {
        if (ModelState.IsValid) {
            var response = await Mediator.Send(new AddDishInfoCommand(request));
            if (response.HasError) return BadRequest(response);
            return Ok(response);
        }
        
        return Ok();
    }
    
    [HttpPost]
    [Route("delete-dish")]
    public async Task<IActionResult> DeleteDish(DeleteDishRequest request)
    {
        if (ModelState.IsValid) {
            var response = await Mediator.Send(new DeleteDishCommand(request));
            if (response.HasError) return BadRequest(response);
            return Ok(response);
        }
        
        return Ok();
    }
    
    [HttpPost]
    [Route("deactive-dish")]
    public async Task<IActionResult> DeactiveDish(DeactiveDishRequest request)
    {
        if (ModelState.IsValid) {
            var response = await Mediator.Send(new DeactivateDishCommand(request));
            if (response.HasError) return BadRequest(response);
            return Ok(response);
        }
        
        return Ok();
    }
    
    [HttpPost]
    [Route("update-dish-info")]
    public async Task<IActionResult> UpdateDishInfo(UpdateDishInfoRequest request)
    {
        if (ModelState.IsValid) {
            var response = await Mediator.Send(new UpdateDishInfoCommand(request));
            if (response.HasError) return BadRequest(response);
            return Ok(response);
        }
        
        return Ok();
    }
    
    [HttpPost]
    [Route("verify-dish")]
    public async Task<IActionResult> VerifyDish(VerifyDishRequest request)
    {
        if (ModelState.IsValid) {
            var response = await Mediator.Send(new VerifyDishCommand(request));
            if (response.HasError) return BadRequest(response);
            return Ok(response);
        }
        
        return Ok();
    }
    
    [HttpGet]
    [Route("get-restaurant-menu")]
    public async Task<IActionResult> GetRestaurantMenu()
    {
        if (ModelState.IsValid) {
            var response = await Mediator.Send(new GetAllDishesCommand(UserId));
            if (response.HasError) return BadRequest(response);
            return Ok(response);
        }
        
        return Ok();
    }
    
    [HttpGet]
    [Route("get-customer-menu")]
    public async Task<IActionResult> GetCustomerMenu(GetAllDishesForCustomerRequest request)
    {
        if (ModelState.IsValid) {
            var response = await Mediator.Send(new GetAllDishesForCustomerQuery(request));
            if (response.HasError) return BadRequest(response);
            return Ok(response);
        }
        
        return Ok();
    }
}