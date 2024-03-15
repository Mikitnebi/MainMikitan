using MainMikitan.Application.Services.Permission;
using MainMikitan.Database.Features.Dish.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests;
using MainMikitan.Domain.Templates;
using MainMikitan.ExternalServicesAdapter.S3ServiceAdapter;
using MainMikitan.InternalServiceAdapterService.Exceptions;

namespace MainMikitan.Application.Features.Dish.Add.Commands;

public class AddDishCommand(List<AddDishRequest> request, int restaurantId, int userId, IEnumerable<int> permissionIds, string userRole) : ICommand
{
    public List<AddDishRequest> Request { get; } = request;
    public int RestaurantId { get; } = restaurantId;
    public IEnumerable<int> PermissionIds { get; } = permissionIds;
    public string UserRole { get; } = userRole;
    public int StaffId { get; } = userId;
}

public class AddDishHandler(IDishCommandRepository dishCommandRepository,
    IPermissionService permissionService,
    IS3Adapter s3Adapter) : ResponseMaker<bool>, ICommandHandler<AddDishCommand>
{

    public async Task<ResponseModel<bool>> Handle(AddDishCommand request, CancellationToken cancellationToken)
    {
        if (!await permissionService.Check(request.StaffId, request.PermissionIds, request.UserRole,
                cancellationToken, request.RestaurantId, 1))
            return Fail(ErrorResponseType.Staff.StaffForbiddenPermission);
        
        ResponseModel<bool> response = new ();
        Dictionary<int, AddDishRequest> dishInfo = new ();

        foreach (var dish in request.Request)
        {
            var dishId = await dishCommandRepository.AddDish(dish);
            
            dishInfo[dishId] = dish;
        }
        
        response.Result = await dishCommandRepository.SaveDishChanges();

        foreach (var dishId in dishInfo.Keys.Where(dish => dishInfo[dish].DishPhoto != null))
        {
            try
            {
                var addImageResponse = await s3Adapter.AddOrUpdateDishImage(dishInfo[dishId].DishPhoto!, request.RestaurantId, dishInfo[dishId].CategoryDishId,
                    dishId);
            }
            catch (MainMikitanException ex)
            {
                response.ErrorType = ErrorResponseType.S3.ImageNotCreatedOrUpdated;
                response.ErrorMessage = ex.Message;
                return response;
            } 
            response.Result = true;
            return response;
        }

        return response;
    }
    
}