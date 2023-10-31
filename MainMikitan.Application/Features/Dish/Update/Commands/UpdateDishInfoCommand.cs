using MainMikitan.Application.Features.Dish.Add.Commands;
using MainMikitan.Database.Features.Dish.Interface;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests;
using MediatR;

namespace MainMikitan.Application.Features.Dish.Update.Commands;

public class UpdateDishInfoCommand : IRequest<ResponseModel<bool>>
{
    public UpdateDishInfoRequest Request { get; }
    public UpdateDishInfoCommand(UpdateDishInfoRequest request)
    {
        Request = request;
    }
    
}

public class UpdateDishInfoHandler : IRequestHandler<UpdateDishInfoCommand, ResponseModel<bool>>
{
    private readonly IDishCommandRepository _dishCommandRepository;
    
    public UpdateDishInfoHandler(IDishCommandRepository dishCommandRepository)
    {
        _dishCommandRepository = dishCommandRepository;
    }

    public async Task<ResponseModel<bool>> Handle(UpdateDishInfoCommand request,
        CancellationToken cancellationToken)
    {
        var response = new ResponseModel<bool>();
        
        response.Result = await _dishCommandRepository.UpdateDishInfo(request.Request);
        
        return response;
    }
}