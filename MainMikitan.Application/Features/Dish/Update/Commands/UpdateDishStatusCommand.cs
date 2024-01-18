using MainMikitan.Database.Features.Dish.Interface;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Application.Features.Dish.Update.Commands;

public class UpdateDishStatusCommand : ICommand
{
    public UpdateDishStatusRequest Request { get; }
    public int UserId { get; }
    
    public UpdateDishStatusCommand(UpdateDishStatusRequest request, int userId)
    {
        Request = request;
        UserId = userId;
    }
}

public class DeactivateDishCommandHandler : ICommandHandler<UpdateDishStatusCommand>
{
    private readonly IDishCommandRepository _dishCommandRepository;
    
    
    public DeactivateDishCommandHandler(IDishCommandRepository dishCommandRepository)
    {
        _dishCommandRepository = dishCommandRepository;
    }
    
    public async Task<ResponseModel<bool>> Handle(UpdateDishStatusCommand request,
        CancellationToken cancellationToken)
    {
        var response = new ResponseModel<bool>();
        
        response.Result = await _dishCommandRepository.UpdateDishStatus(request.Request, request.UserId);
        
        return response;
    }
}