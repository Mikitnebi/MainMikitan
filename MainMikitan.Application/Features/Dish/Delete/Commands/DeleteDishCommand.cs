using MainMikitan.Database.Features.Dish.Interface;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests;
using MediatR;

namespace MainMikitan.Application.Features.Dish.Delete.Commands;

public class DeleteDishCommand : IRequest<ResponseModel<bool>>
{
    public DeleteDishRequest Request { get; }
    public DeleteDishCommand(DeleteDishRequest request)
    {
        Request = request;
    }
}

public class DeleteDishHandler : IRequestHandler<DeleteDishCommand, ResponseModel<bool>>
{
    private readonly IDishCommandRepository _dishCommandRepository;
    
    public DeleteDishHandler(IDishCommandRepository dishCommandRepository)
    {
        _dishCommandRepository = dishCommandRepository;
    }

    public async Task<ResponseModel<bool>> Handle(DeleteDishCommand request,
        CancellationToken cancellationToken)
    {
        var response = new ResponseModel<bool>();
        
        response.Result = await _dishCommandRepository.DeleteDish(request.Request);
        
        return response;
    }
}