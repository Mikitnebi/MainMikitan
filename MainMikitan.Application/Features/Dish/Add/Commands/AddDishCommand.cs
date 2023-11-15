using MainMikitan.Database.Features.Dish.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests;
using MainMikitan.ExternalServicesAdapter.S3ServiceAdapter;
using MainMikitan.InternalServiceAdapterService.Exceptions;
using MediatR;

namespace MainMikitan.Application.Features.Dish.Add.Commands;

public class AddDishCommand : IRequest<ResponseModel<bool>>
{
    public List<AddDishRequest> Request { get; } 
    public int RestaurantId { get; }
    
    public AddDishCommand(List<AddDishRequest> request, int restaurantId)
    {
        Request = request;
        RestaurantId = restaurantId;
    }
}

public class AddDishHandler : IRequestHandler<AddDishCommand, ResponseModel<bool>>
{
    private readonly IDishCommandRepository _dishCommandRepository;
    private readonly IS3Adapter _s3adapter;
    
    public AddDishHandler(IDishCommandRepository dishCommandRepository, 
        IS3Adapter s3Adapter)
    {
        _dishCommandRepository = dishCommandRepository;
        _s3adapter = s3Adapter;
    }

    public async Task<ResponseModel<bool>> Handle(AddDishCommand request, CancellationToken cancellationToken)
    {
        ResponseModel<bool> response = new ();
        Dictionary<int, AddDishRequest> dishInfo = new ();

        foreach (var dish in request.Request)
        {
            var dishId = await _dishCommandRepository.AddDish(dish);
            
            dishInfo[dishId] = dish;
        }
        
        response.Result = await _dishCommandRepository.SaveDishChanges();

        foreach (var dishId in dishInfo.Keys.Where(dish => dishInfo[dish].DishPhoto != null))
        {
            try
            {
                var addImageResponse = await _s3adapter.AddOrUpdateDishImage(dishInfo[dishId].DishPhoto, request.RestaurantId, dishInfo[dishId].CategoryDishId,
                    dishId);
            }
            catch (MainMikitanException ex)
            {
                response.ErrorType = ErrorType.S3.ImageNotCreatedOrUpdated;
                response.ErrorMessage = ex.Message;
                return response;
            } 
            response.Result = true;
            return response;
        }

        return response;
    }
    
}