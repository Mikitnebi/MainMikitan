using MainMikitan.Database.Features.Dish.Interface;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Application.Features.Dish.Update.Commands;

public class DeactivateDishCommand : ICommand
{
    public DeactiveDishRequest Request { get; }
    
    public DeactivateDishCommand(DeactiveDishRequest request)
    {
        Request = request;
    }
}

public class DeactivateDishCommandHandler : ICommandHandler<DeactivateDishCommand>
{
    private readonly IDishCommandRepository _dishCommandRepository;
    
    public DeactivateDishCommandHandler(IDishCommandRepository dishCommandRepository)
    {
        _dishCommandRepository = dishCommandRepository;
    }
    
    public async Task<ResponseModel<bool>> Handle(DeactivateDishCommand request,
        CancellationToken cancellationToken)
    {
        var response = new ResponseModel<bool>();
        
        response.Result = await _dishCommandRepository.DeactiveDish(request.Request);
        
        return response;
    }
}