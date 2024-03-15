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

[Authorized(Enums.RoleId.Manager)]
public class DishController(IMediator mediator) : MainController(mediator)
{
    [HttpPost("add-dish-category")]
    public async Task<IActionResult> AddCategoryDish(List<AddCategoryDishRequest> request)
    {
        if (!ModelState.IsValid) return Ok();
        var response = await Mediator.Send(new AddCategoryDishCommand(request, new []{ (int)Enums.RestaurantPermissionId.Dish }, UserRole!, UserId, RestaurantId));
        if (response.HasError) return BadRequest(response);
        
        return Ok(response);
    }
    
    [HttpPost("add-dish")]
    public async Task<IActionResult> AddDish(List<AddDishRequest> request)
    {
        if (!ModelState.IsValid) return Ok();
        var response = await Mediator.Send(new AddDishCommand(request, RestaurantId, UserId, new []{ (int)Enums.RestaurantPermissionId.Dish }, UserRole!));
        if (response.HasError) return BadRequest(response);
        
        return Ok(response);
    }
    
    [HttpPost("add-dish-info")]
    public async Task<IActionResult> AddDishInfo(List<AddDishInfoRequest> request)
    {
        if (!ModelState.IsValid) return Ok();
        var response = await Mediator.Send(new AddDishInfoCommand(request, RestaurantId, new []{ (int)Enums.RestaurantPermissionId.Dish }, UserRole!, UserId));
        if (response.HasError) return BadRequest(response);
        
        return Ok(response);
    }
    
    [HttpPost("revoke-dish")]
    public async Task<IActionResult> RevokeDish(DeleteDishRequest request)
    {
        if (!ModelState.IsValid) return Ok();
        request.IsDeletedStatus = false;
        var response = await Mediator.Send(new DeleteDishCommand(request, RestaurantId, UserId, UserRole!, new []{ (int)Enums.RestaurantPermissionId.Dish }));
        if (response.HasError) return BadRequest(response);
        
        return Ok(response);
    }
    
    [HttpPost("delete-dish")]
    public async Task<IActionResult> DeleteDish(DeleteDishRequest request)
    {
        if (!ModelState.IsValid) return Ok();
        request.IsDeletedStatus = true;
        var response = await Mediator.Send(new DeleteDishCommand(request, RestaurantId, UserId, UserRole!, new []{ (int)Enums.RestaurantPermissionId.Dish }));
        if (response.HasError) return BadRequest(response);
        
        return Ok(response);
    }
    
    [HttpPost("activate-dish")]
    public async Task<IActionResult> ActivateDish(UpdateDishStatusRequest request)
    {
        if (!ModelState.IsValid) return Ok();
        request.IsActiveStatus = true;
        var response = await Mediator.Send(new UpdateDishStatusCommand(request, UserId, RestaurantId, UserRole!, new []{ (int)Enums.RestaurantPermissionId.Dish }));
        if (response.HasError) return BadRequest(response);
        
        return Ok(response);
    }
    
    [HttpPost("deactivate-dish")]
    public async Task<IActionResult> DeactivateDish(UpdateDishStatusRequest request)
    {
        if (!ModelState.IsValid) return Ok();
        request.IsActiveStatus = false;
        var response = await Mediator.Send(new UpdateDishStatusCommand(request, UserId, RestaurantId, UserRole!, new []{ (int)Enums.RestaurantPermissionId.Dish }));
        if (response.HasError) return BadRequest(response);
        
        return Ok(response);
    }
    
    [HttpPost("update-dish-info")]
    public async Task<IActionResult> UpdateDishInfo(UpdateDishInfoRequest request)
    {
        if (!ModelState.IsValid) return Ok();
        var response = await Mediator.Send(new UpdateDishInfoCommand(request, UserId, RestaurantId, UserRole!, new []{ (int)Enums.RestaurantPermissionId.Dish }));
        if (response.HasError) return BadRequest(response);
        
        return Ok(response);
    }
    
    [HttpPost("verify-dish")]
    public async Task<IActionResult> VerifyDish(VerifyDishRequest request)
    {
        if (!ModelState.IsValid) return Ok();
        var response = await Mediator.Send(new VerifyDishCommand(request, RestaurantId, UserId, UserRole!, new []{ (int)Enums.RestaurantPermissionId.Dish }));
        if (response.HasError) return BadRequest(response);
        
        return Ok(response);
    }
    
    [HttpGet("get-restaurant-menu")]
    public async Task<IActionResult> GetRestaurantMenu()
    {
        if (!ModelState.IsValid) return Ok();
        var response = await Mediator.Send(new GetAllDishesCommand(RestaurantId, UserId, UserRole!, new []{ (int)Enums.RestaurantPermissionId.Dish }));
        if (response.HasError) return BadRequest(response);
        
        return Ok(response);
    }
    
    [HttpGet("get-customer-menu")]
    public async Task<IActionResult> GetCustomerMenu(GetAllDishesForCustomerRequest request)
    {
        if (!ModelState.IsValid) return Ok();
        var response = await Mediator.Send(new GetAllDishesForCustomerQuery(request));
        if (response.HasError) return BadRequest(response);
        
        return Ok(response);
    }
}