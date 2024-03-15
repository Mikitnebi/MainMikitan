using MainMikitan.Application.Services.Permission;
using MainMikitan.Database.Features.Dish.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Application.Features.Dish.Delete.Commands;

public class DeleteDishCommand(DeleteDishRequest request, int restaurantId, int userId, string userRole, IEnumerable<int> permissionIds) : ICommand
{
    public DeleteDishRequest Request { get; } = request;
    public int RestaurantId { get; } = restaurantId;
    public IEnumerable<int> PermissionIds { get; } = permissionIds;
    public string UserRole { get; } = userRole;
    public int StaffId { get; } = userId;
}

public class DeleteDishHandler(IDishCommandRepository dishCommandRepository, IPermissionService permissionService) : ResponseMaker<bool>, ICommandHandler<DeleteDishCommand>
{
    public async Task<ResponseModel<bool>> Handle(DeleteDishCommand request,
        CancellationToken cancellationToken)
    {
        if (!await permissionService.Check(request.StaffId, request.PermissionIds, request.UserRole,
                cancellationToken, request.RestaurantId, 1))
            return Fail(ErrorResponseType.Staff.StaffForbiddenPermission);
        
        var response = new ResponseModel<bool>
        {
            Result = await dishCommandRepository.DeleteDish(request.Request)
        };

        return response;
    }
}