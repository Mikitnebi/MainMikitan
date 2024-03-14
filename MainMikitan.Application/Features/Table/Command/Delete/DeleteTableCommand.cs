using MainMikitan.Application.Services.Permission;
using MainMikitan.Database.Features.Table.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.TableRequests;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Application.Features.Table.Command.Delete;

public class DeleteTableCommand(DeleteTableRequest request, int restaurantId, int userId, string userRole, IEnumerable<int> permissionIds) : ICommand
{
    public DeleteTableRequest Request { get; } = request;
    public int RestaurantId { get; } = restaurantId;
    public int UserId { get; } = userId;
    public IEnumerable<int> PermissionIds { get; } = permissionIds;
    public string UserRole { get; } = userRole;
}

public class DeleteTableCommandHandler(ITableCommandRepository tableCommandRepository, 
    ITableEnvironmentCommandRepository tableEnvironmentCommandRepository,
    IPermissionService permissionService)
    : ResponseMaker, ICommandHandler<DeleteTableCommand>
{

    public async Task<ResponseModel<bool>> Handle(DeleteTableCommand command,
        CancellationToken cancellationToken)
    {
        if (!await permissionService.Check(command.UserId, command.PermissionIds, command.UserRole,
                cancellationToken, command.RestaurantId, 1))
            return Fail(ErrorResponseType.Staff.StaffForbiddenPermission);
        
        try
        {
            await tableEnvironmentCommandRepository.DeleteTableEnvironmentInfo(command.Request.TableId, cancellationToken);
            await tableCommandRepository.DeleteTable(command.Request.TableId, cancellationToken);
            
            await tableCommandRepository.SaveChanges();

            return Success();
        }
        catch (Exception e)
        {
            return Unexpected(e);
        }
    }
}