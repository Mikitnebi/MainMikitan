using MainMikitan.Database.Features.Table.Interface;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.TableRequests;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Application.Features.Table.Command.Delete;

public class DeleteTableCommand(DeleteTableRequest request) : ICommand
{
    public DeleteTableRequest Request { get; } = request;
}

public class DeleteTableCommandHandler(ITableCommandRepository tableCommandRepository, ITableEnvironmentCommandRepository tableEnvironmentCommandRepository)
    : ResponseMaker, ICommandHandler<DeleteTableCommand>
{

    public async Task<ResponseModel<bool>> Handle(DeleteTableCommand command,
        CancellationToken cancellationToken)
    {
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