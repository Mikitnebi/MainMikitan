using MainMikitan.Application.Services.Permission;
using MainMikitan.Database.Features.Dish.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Application.Features.Dish.Add.Commands;

public class AddDishInfoCommand(List<AddDishInfoRequest> request, int restaurantId, IEnumerable<int> permissionIds, string userRole, int userId) : ICommand
{
    public List<AddDishInfoRequest> Request { get; } = request;
    public int RestaurantId { get; } = restaurantId;
    public IEnumerable<int> PermissionIds { get; } = permissionIds;
    public string UserRole { get; } = userRole;
    public int StaffId { get; } = userId;
}

public class AddDishInfoHandler(IDishCommandRepository dishCommandRepository, IPermissionService permissionService) : ResponseMaker<bool>, ICommandHandler<AddDishInfoCommand>
{
    
    public async Task<ResponseModel<bool>> Handle(AddDishInfoCommand request,
        CancellationToken cancellationToken)
    {
        if (!await permissionService.Check(request.StaffId, request.PermissionIds, request.UserRole,
                cancellationToken, request.RestaurantId, 1))
            return Fail(ErrorResponseType.Staff.StaffForbiddenPermission);
        
        var response = new ResponseModel<bool>();
        
        foreach (var dishInfo in request.Request)
        {
            var addResponse = await dishCommandRepository.AddDishInfo(dishInfo);
            if (addResponse >= 1) continue;
            response.ErrorMessage = "TODO: შესაქმნელია Error Type";
            response.Result = false;

            return response;
        }
        
        response.Result = await dishCommandRepository.SaveDishChanges();

        return response;
    }
}