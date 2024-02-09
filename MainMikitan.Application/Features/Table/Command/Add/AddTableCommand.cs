using MainMikitan.Database.Features.Table.Interface;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Models.Restaurant.TableManagement;
using MainMikitan.Domain.Requests.TableRequests;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Application.Features.Table.Command.Add;

public class AddTableCommand(AddTableRequest request, int restaurantId) : ICommand
{
    public AddTableRequest Request { get; } = request;
    public int RestaurantId { get; } = restaurantId;
}

public class AddTableCommandHandler(ITableCommandRepository tableCommandRepository, ITableEnvironmentCommandRepository tableEnvironmentCommandRepository)
    : ResponseMaker, ICommandHandler<AddTableCommand>
{

    public async Task<ResponseModel<bool>> Handle(AddTableCommand command,
        CancellationToken cancellationToken)
    {
        var restaurantId = command.RestaurantId;
        var request = command.Request;
        var tableInfoEntity = new TableInfoEntity()
        {
            RestaurantId = restaurantId,
            TableNumber = request.TableNumber,
            MaxPlace = request.MaxPlace,
            MinPlace = request.MinPlace,
            TableType = request.TableType,
            XCoordinate = request.XCoordinate,
            YCoordinate = request.YCoordinate
        };
        try
        {
            var tableAddResponse = await tableCommandRepository.AddTable(tableInfoEntity, cancellationToken);
            var resultTableInfoSave = await tableCommandRepository.SaveChanges();
            if (!resultTableInfoSave)
            {
                return Fail("TABLE_INFO_WAS_NOT_ADDED");
            }
            
            TableEnvironmentEntity tableEnvironmentInfo = new()
            {
                TableId = tableAddResponse.Result!.Id
            };
            
            foreach (var environment in request.EnvironmentId)
            {
                tableEnvironmentInfo.EnvironmentId = environment;
                
                await tableEnvironmentCommandRepository.AddTableEnvironmentInfo(tableEnvironmentInfo, cancellationToken);
            }

            var result = await tableEnvironmentCommandRepository.SaveChanges();

            return !result ? Fail("TABLE_ENVIRONMENT_INFO_WAS_NOT_ADDED") : Success();
        }
        catch (Exception e)
        {
            await tableCommandRepository.DeleteTable(tableInfoEntity.Id, cancellationToken);
            await tableCommandRepository.SaveChanges();
            
            return Unexpected(e);
        }
    }
}