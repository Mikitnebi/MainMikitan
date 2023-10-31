using MainMikitan.Database.Features.Dish.Interface;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests;
using MediatR;

namespace MainMikitan.Application.Features.Dish.Add.Commands;

public class AddDishCommand : IRequest<ResponseModel<bool>>
{
    public List<AddDishRequest> Request { get; } 
    
    public AddDishCommand(List<AddDishRequest> request)
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

        foreach (var dish in request.Request)
        {
            await _dishCommandRepository.AddDish(dish);
        }

        response.Result = await _dishCommandRepository.SaveDishChanges();

        return response;
    }
    
}