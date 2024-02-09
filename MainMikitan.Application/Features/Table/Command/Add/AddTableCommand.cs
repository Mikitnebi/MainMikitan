using MainMikitan.Database.DbContext;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.TableRequests;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Application.Features.Table.Command.Add;

public class AddTableCommand(AddTableRequest request, int restaurantId) : ICommand
{
    private AddTableRequest Request { get; } = request;
    private int RestaurantId { get; } = restaurantId;

    public class AddTableCommandHandler : ResponseMaker, ICommandHandler<AddTableCommand>
    {
        
        public async Task<ResponseModel<bool>> Handle(AddTableCommand command,
            CancellationToken cancellationToken)
        {
            var restaurantId = command.RestaurantId;
            var request = command.Request;
            
            return new ResponseModel<bool>();
        }
    }
}