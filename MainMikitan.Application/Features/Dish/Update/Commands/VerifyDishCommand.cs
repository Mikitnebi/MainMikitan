using MainMikitan.Application.Services.Permission;
using MainMikitan.Database.Features.Dish.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Application.Features.Dish.Update.Commands;

public class VerifyDishCommand(VerifyDishRequest request, int restaurantId, int userId, string userRole, IEnumerable<int> permissionIds) : ICommand
{
    public VerifyDishRequest Request { get; } = request;
    public int UserId { get; } = userId;
    public int RestaurantId { get; } = restaurantId;
    public IEnumerable<int> PermissionIds { get; } = permissionIds;
    public string UserRole { get; } = userRole;
}

public class VerifyDishHandler(IDishCommandRepository dishCommandRepository, IPermissionService permissionService) : ResponseMaker<bool>, ICommandHandler<VerifyDishCommand>
{
    public async Task<ResponseModel<bool>> Handle(VerifyDishCommand request,
        CancellationToken cancellationToken)
    {
        if (!await permissionService.Check(request.UserId, request.PermissionIds, request.UserRole,
                cancellationToken, request.RestaurantId, 1))
            return Fail(ErrorResponseType.Staff.StaffForbiddenPermission);
        
        var response = new ResponseModel<bool>
        {
            Result = await dishCommandRepository.VerifyDish(request.Request)
        };

        return response;
    }
}