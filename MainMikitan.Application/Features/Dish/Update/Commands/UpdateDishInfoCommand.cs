using MainMikitan.Application.Services.Permission;
using MainMikitan.Database.Features.Dish.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Application.Features.Dish.Update.Commands;

public class UpdateDishInfoCommand(UpdateDishInfoRequest request, int userId, int restaurantId, string userRole, IEnumerable<int> permissionIds) : ICommand
{
    public UpdateDishInfoRequest Request { get; } = request;
    public int UserId { get; } = userId;
    public int RestaurantId { get; } = restaurantId;
    public IEnumerable<int> PermissionIds { get; } = permissionIds;
    public string UserRole { get; } = userRole;
}

public class UpdateDishInfoHandler(IDishCommandRepository dishCommandRepository, IPermissionService permissionService) : ResponseMaker<bool>, ICommandHandler<UpdateDishInfoCommand>
{
    public async Task<ResponseModel<bool>> Handle(UpdateDishInfoCommand request,
        CancellationToken cancellationToken)
    {
        if (!await permissionService.Check(request.UserId, request.PermissionIds, request.UserRole,
                cancellationToken, request.RestaurantId, 1))
            return Fail(ErrorResponseType.Staff.StaffForbiddenPermission);
        
        var response = new ResponseModel<bool>
        {
            Result = await dishCommandRepository.UpdateDishInfo(request.Request, request.UserId)
        };

        return response;
    }
}