using Amazon.Runtime.Internal;
using MainMikitan.Database.Features.Dish.Interface;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests;
using MediatR;

namespace MainMikitan.Application.Features.Dish.Add.Commands;

public class AddDishCommand : IRequest<ResponseModel<bool>>
{
    public AddDishRequest Request { get; } 
    
    public AddDishCommand(AddDishRequest request)
    {
        Request = request;
    }
}

public class AddDishHandler : IRequestHandler<AddDishCommand, ResponseModel<bool>>
{
    private readonly IDishCommandRepository _dishCommandRepository;
    
    public AddDishHandler(IDishCommandRepository dishCommandRepository)
    {
        _dishCommandRepository = dishCommandRepository;
    }

    public async Task<ResponseModel<bool>> Handle(AddDishCommand request, CancellationToken cancellationToken)
    {
        var response = new ResponseModel<bool>();
        await _dishCommandRepository.AddDish(request.Request);
        response.Result = await _dishCommandRepository.SaveDishChanges();

        return response;
    }
    
}