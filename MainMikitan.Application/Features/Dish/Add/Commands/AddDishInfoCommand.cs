using MainMikitan.Database.Features.Dish.Interface;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Application.Features.Dish.Add.Commands;

public class AddDishInfoCommand : ICommand
{
    public List<AddDishInfoRequest> Request { get; }

    public AddDishInfoCommand(List<AddDishInfoRequest> request)
    {
        Request = request;
    }
}

public class AddDishInfoHandler : ICommandHandler<AddDishInfoCommand>
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
            if (addResponse >= 1) continue;
            response.ErrorMessage = "TODO: შესაქმნელია Error Type";
            response.Result = false;

            return response;
        }
        
        response.Result = await _dishCommandRepository.SaveDishChanges();

        return response;
    }
}