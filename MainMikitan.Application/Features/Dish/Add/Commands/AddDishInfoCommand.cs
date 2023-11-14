using MainMikitan.Database.Features.Dish.Interface;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests;
using MediatR;

namespace MainMikitan.Application.Features.Dish.Add.Commands;

public class AddDishInfoCommand : IRequest<ResponseModel<bool>>
{
    public List<AddDishInfoRequest> Request { get; }

    public AddDishInfoCommand(List<AddDishInfoRequest> request)
    {
        Request = request;
    }
}

public class AddDishInfoHandler : IRequestHandler<AddDishInfoCommand, ResponseModel<bool>>
{
    private readonly IDishCommandRepository _dishCommandRepository;
    
    public AddDishInfoHandler(IDishCommandRepository dishCommandRepository)
    {
        _dishCommandRepository = dishCommandRepository;
        
    }
    
    public async Task<ResponseModel<bool>> Handle(AddDishInfoCommand request,
        CancellationToken cancellationToken)
    {
        var response = new ResponseModel<bool>();
        
        foreach (var dishInfo in request.Request)
        {
            var addResponse = await _dishCommandRepository.AddDishInfo(dishInfo);
            if (addResponse < 1)
            {
                response.ErrorMessage = "TODO: შესაქმნელია Error Type";
                response.Result = false;

                return response;
            }
        }
        
        response.Result = await _dishCommandRepository.SaveDishChanges();

        return response;
    }
}