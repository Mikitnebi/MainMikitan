using Amazon.Runtime.Internal;
using MainMikitan.Database.Features.Dish.Interface;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests;
using MediatR;

namespace MainMikitan.Application.Features.Dish.Update.Commands;

public class DeactiveDishCommand : IRequest<ResponseModel<bool>>
{
    public DeactiveDishRequest Request { get; }
    
    public DeactiveDishCommand(DeactiveDishRequest request)
    {
        Request = request;
    }
}

public class DeactiveDishHandler : IRequestHandler<DeactiveDishCommand, ResponseModel<bool>>
{
    private readonly IDishCommandRepository _dishCommandRepository;
    
    public DeactiveDishHandler(IDishCommandRepository dishCommandRepository)
    {
        _dishCommandRepository = dishCommandRepository;
    }
    
    public async Task<ResponseModel<bool>> Handle(DeactiveDishCommand request,
        CancellationToken cancellationToken)
    {
        var response = new ResponseModel<bool>();
        
        response.Result = await _dishCommandRepository.DeactiveDish(request.Request);
        
        return response;
    }
}