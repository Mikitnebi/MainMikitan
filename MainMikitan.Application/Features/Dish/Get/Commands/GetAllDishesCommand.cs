using MainMikitan.Database.Features.Dish.Interface;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests;
using MainMikitan.Domain.Responses.DishResponses;
using MediatR;

namespace MainMikitan.Application.Features.Dish.Get.Commands;

public class GetAllDishesCommand : IRequest<ResponseModel<List<GetDishInfoResponse>>>
{
    public GetAllDishesRequest Request { get; }

    public GetAllDishesCommand(GetAllDishesRequest request)
    {
        Request = request;
    }
}

public class GetAllDishesHandler : IRequestHandler<GetAllDishesCommand, ResponseModel<List<GetDishInfoResponse>>>
{
    private readonly IDishCommandRepository _dishCommandRepository;
    
    public GetAllDishesHandler(IDishCommandRepository dishCommandRepository)
    {
        _dishCommandRepository = dishCommandRepository;
    }

    public async Task<ResponseModel<List<GetDishInfoResponse>>> Handle(GetAllDishesCommand request,
        CancellationToken cancellationToken)
    {
        var responseModel = new ResponseModel<List<GetDishInfoResponse>>
        {
            Result = _dishCommandRepository.GetAllDishes(request.Request)
        };

        return responseModel;
    }
}