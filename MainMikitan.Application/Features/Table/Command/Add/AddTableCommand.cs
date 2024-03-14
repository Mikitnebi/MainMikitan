using AutoMapper;
using MainMikitan.Database.Features.Table.Interface;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Models.Restaurant.TableManagement;
using MainMikitan.Domain.Requests.TableRequests;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Application.Features.Table.Command.Add;

public class AddTableCommand(List<AddTableRequest> request, int restaurantId) : ICommand
{
    public List<AddTableRequest> Request { get; } = request;
    public int RestaurantId { get; } = restaurantId;
}

public class AddTableCommandHandler(ITableCommandRepository tableCommandRepository, 
    ITableEnvironmentCommandRepository tableEnvironmentCommandRepository,
    ITableQueryRepository tableQueryRepository,
    IMapper mapper)
    : ResponseMaker, ICommandHandler<AddTableCommand>
{

    public async Task<ResponseModel<bool>> Handle(AddTableCommand command,
        CancellationToken cancellationToken)
    {
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