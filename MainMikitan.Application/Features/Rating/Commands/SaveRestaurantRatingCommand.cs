using AutoMapper;
using MainMikitan.Database.Features.Rating.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Models.Rating;
using MainMikitan.Domain.Requests.Rating;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Application.Features.Rating.Commands;

public class SaveRestaurantRatingCommand(int userId, SaveRestaurantRatingRequest request) : ICommand<bool>
{
    public int UserId { get; set; } = userId;
    public SaveRestaurantRatingRequest Request { get; set; } = request;
}

public class SaveRestaurantRatingCommandHandler(IRestaurantRatingCommandRepository restaurantRatingCommandRepository,
    IMapper mapper)
    : ResponseMaker, ICommandHandler<SaveRestaurantRatingCommand, bool>
{
    public async Task<ResponseModel<bool>> Handle(SaveRestaurantRatingCommand command,CancellationToken cancellationToken)
    {
        var restaurantRatingEntity = mapper.Map<RestaurantRatingEntity>(command.Request);
        restaurantRatingEntity.UserId = command.UserId;
        restaurantRatingEntity.CreatedAt = DateTime.Now;
        
        await restaurantRatingCommandRepository.SaveRating(restaurantRatingEntity);
        return await restaurantRatingCommandRepository.SaveChangesAsync() ? Success() : Fail(ErrorResponseType.Rating.RatingSaveFail);
    }
}