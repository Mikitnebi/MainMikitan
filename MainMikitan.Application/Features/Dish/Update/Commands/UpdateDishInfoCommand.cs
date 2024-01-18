using MainMikitan.Database.Features.Dish.Interface;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Application.Features.Dish.Update.Commands;

public class UpdateDishInfoCommand : ICommand
{
    public UpdateDishInfoRequest Request { get; }
    public int UserId { get; }
    public UpdateDishInfoCommand(UpdateDishInfoRequest request, int userId)
    {
        Request = request;
        UserId = userId;
    }
    
}

public class UpdateDishInfoHandler : ICommandHandler<UpdateDishInfoCommand>
{
    private readonly IDishCommandRepository _dishCommandRepository;
    
    public UpdateDishInfoHandler(IDishCommandRepository dishCommandRepository)
    {
        _dishCommandRepository = dishCommandRepository;
    }

    public async Task<ResponseModel<bool>> Handle(UpdateDishInfoCommand request,
        CancellationToken cancellationToken)
    {
        var response = new ResponseModel<bool>
        {
            Result = await _dishCommandRepository.UpdateDishInfo(request.Request, request.UserId)
        };

        return response;
    }
}