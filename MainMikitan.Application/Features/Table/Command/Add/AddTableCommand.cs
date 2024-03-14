using AutoMapper;
using MainMikitan.Application.Services.Permission;
using MainMikitan.Database.Features.Table.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Models.Restaurant.TableManagement;
using MainMikitan.Domain.Requests.TableRequests;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Application.Features.Table.Command.Add;

public class AddTableCommand(List<AddTableRequest> request, int restaurantId, int userId, string userRole, IEnumerable<int> permissionIds) : ICommand
{
    public List<AddTableRequest> Request { get; } = request;
    public int RestaurantId { get; } = restaurantId;
    public int UserId { get; } = userId;
    public IEnumerable<int> PermissionIds { get; } = permissionIds;
    public string UserRole { get; } = userRole;
}

public class AddTableCommandHandler(ITableCommandRepository tableCommandRepository, 
    ITableEnvironmentCommandRepository tableEnvironmentCommandRepository,
    ITableQueryRepository tableQueryRepository,
    IPermissionService permissionService,
    IMapper mapper)
    : ResponseMaker, ICommandHandler<AddTableCommand>
{

    public async Task<ResponseModel<bool>> Handle(AddTableCommand command,
        CancellationToken cancellationToken)
    {
        if (!await permissionService.Check(command.UserId, command.PermissionIds, command.UserRole,
                cancellationToken, command.RestaurantId, 1))
            return Fail(ErrorResponseType.Staff.StaffForbiddenPermission);
        
        var restaurantId = command.RestaurantId;
        var addTableRequests = command.Request;
        var tableInfoEntities = mapper.Map<List<TableInfoEntity>>(addTableRequests);
        foreach (var table in tableInfoEntities)
        {
            table.RestaurantId = restaurantId;
        }
        try
        {
            foreach (var tableEntity in tableInfoEntities)
            {
                var tableNumerationExists =
                    await tableQueryRepository.GetSingleTableByNumeration(tableEntity.TableNumber, restaurantId, cancellationToken);

                if (tableNumerationExists.Result is not null)
                    return Fail("TABLE_WITH_PROVIDED_NUMERATION_EXISTS");
                
                var tableAddResponse = await tableCommandRepository.AddTable(tableEntity, cancellationToken);
                var resultTableInfoSave = await tableCommandRepository.SaveChanges();
                if (!resultTableInfoSave)
                    return Fail("TABLE_INFO_WAS_NOT_ADDED");
                
                TableEnvironmentEntity tableEnvironmentInfo = new()
                {
                    TableId = tableAddResponse.Result!.Id
                };
            
                foreach (var environment in addTableRequests.FirstOrDefault( r => r.TableNumber == tableEntity.TableNumber)!.EnvironmentIds)
                {
                    tableEnvironmentInfo.EnvironmentId = environment;
                
                    await tableEnvironmentCommandRepository.AddTableEnvironmentInfo(tableEnvironmentInfo, cancellationToken);
                }
            }
            
            var result = await tableEnvironmentCommandRepository.SaveChanges();

            return !result ? Fail("TABLE_ENVIRONMENT_INFO_WAS_NOT_ADDED") : Success();
        }
        catch (Exception e)
        {
            foreach (var tableInfo in tableInfoEntities)
            {
                await tableCommandRepository.DeleteTable(tableInfo.Id, cancellationToken);
            }
            await tableCommandRepository.SaveChanges();
            return Unexpected(e);
        }
    }
}