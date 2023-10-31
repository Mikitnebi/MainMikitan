using MainMikitan.Database.Features.Dish.Interface;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests;
using MediatR;

namespace MainMikitan.Application.Features.Dish.Add.Commands;

public class AddCategoryDishCommand : IRequest<ResponseModel<bool>>
{
    public List<AddCategoryDishRequest> Request { get; }
    public AddCategoryDishCommand(List<AddCategoryDishRequest> request)
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

        foreach (var dishCategory in request.Request)
        {
            await _dishCommandRepository.AddDishCategory(dishCategory);
        }
        
        response.Result = await _dishCommandRepository.SaveDishChanges();

        return response;
    }
}
