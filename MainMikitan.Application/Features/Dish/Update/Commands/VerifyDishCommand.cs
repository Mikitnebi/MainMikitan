using MainMikitan.Database.Features.Dish.Interface;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Application.Features.Dish.Update.Commands;

public class VerifyDishCommand : ICommand
{
    public VerifyDishRequest Request { get; }
    
    public VerifyDishCommand(VerifyDishRequest request)
    {
        Request = request;
    }
}

public class VerifyDishHandler : ICommandHandler<VerifyDishCommand>
{
    private readonly IDishCommandRepository _dishCommandRepository;
    
    public VerifyDishHandler(IDishCommandRepository dishCommandRepository)
    {
        _dishCommandRepository = dishCommandRepository;
    }

    public async Task<ResponseModel<bool>> Handle(VerifyDishCommand request,
        CancellationToken cancellationToken)
    {
        var response = new ResponseModel<bool>
        {
            Result = await _dishCommandRepository.VerifyDish(request.Request)
        };

        return response;
    }
}