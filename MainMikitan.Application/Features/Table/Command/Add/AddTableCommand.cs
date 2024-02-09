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

public class AddTableCommandHandler(ITableCommandRepository tableCommandRepository)
    : ResponseMaker, ICommandHandler<AddTableCommand>
{

    public async Task<ResponseModel<bool>> Handle(AddTableCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var restaurantId = command.RestaurantId;
            var request = command.Request;

            var tableEnvironmentResponse = new object();

            var tableInfoEntity = new TableInfoEntity()
            {
                RestaurantId = restaurantId,
                TableNumber = request.TableNumber,
                MaxPlace = request.MaxPlace,
                MinPlace = request.MinPlace,
                TableType = request.TableType,
                XCoordinate = request.XCoordinate,
                YCoordinate = request.YCoordinate,
                TableEnvironmentListId =
                    0 // TODO: აქამდე უნდა შეიქმნას მაგიდის Environment ინფორმაცია და აქ ჩაინსერტდეს Id
            };

            var tableAddResponse = await tableCommandRepository.AddTable(tableInfoEntity, cancellationToken);

            return new ResponseModel<bool>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}