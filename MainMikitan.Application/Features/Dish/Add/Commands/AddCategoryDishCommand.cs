using MainMikitan.Application.Services.Permission;
using MainMikitan.Database.Features.Dish.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Application.Features.Dish.Add.Commands;

public class AddCategoryDishCommand(List<AddCategoryDishRequest> request, IEnumerable<int> permissionIds, string userRole, int userId, int restaurantId) : ICommand
{
    public IEnumerable<int> PermissionIds { get; set; } = permissionIds;
    public string UserRole { get; set; } = userRole;
    public int StaffId { get; set; } = userId;
    public int RestaurantId { get; set; } = restaurantId;
    public List<AddCategoryDishRequest> Request { get; set; } = request;
    
    
}

public class AddCategoryDishHandler(IDishCommandRepository dishCommandRepository, IPermissionService permissionService) : ResponseMaker<bool>, ICommandHandler<AddCategoryDishCommand>
{
    public async Task<ResponseModel<bool>> Handle(AddCategoryDishCommand request,
        CancellationToken cancellationToken)
    {
        if (!await permissionService.Check(request.StaffId, request.PermissionIds, request.UserRole,
                cancellationToken, request.RestaurantId, 1))
            return Fail(ErrorResponseType.Staff.StaffForbiddenPermission);
        
        var response = new ResponseModel<bool>();

        foreach (var dishCategory in request.Request)
        {
            await dishCommandRepository.AddDishCategory(dishCategory);
        }
        
        response.Result = await dishCommandRepository.SaveDishChanges();

        return response;
    }
}
