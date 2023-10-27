using MainMikitan.Database.Features.Dish.Interface;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests;
using MediatR;

namespace MainMikitan.Application.Features.Dish.Add.Commands;

public class AddCategoryDishCommand : IRequest<ResponseModel<bool>>
{
    public AddCategoryDishRequest Request { get; }
    public AddCategoryDishCommand(AddCategoryDishRequest request)
    {
        Request = request;
    }
    
}

public class AddCategoryDishHandler : IRequestHandler<AddCategoryDishCommand, ResponseModel<bool>>
{
    private readonly IDishCommandRepository _dishCommandRepository;
    
    public AddCategoryDishHandler(IDishCommandRepository dishCommandRepository)
    {
        _dishCommandRepository = dishCommandRepository;
    }

    public async Task<ResponseModel<bool>> Handle(AddCategoryDishCommand request,
        CancellationToken cancellationToken)
    {
        var response = new ResponseModel<bool>();
        
        await _dishCommandRepository.AddDishCategory(request.Request);
        response.Result = await _dishCommandRepository.SaveDishChanges();

        return response;
    }
}
