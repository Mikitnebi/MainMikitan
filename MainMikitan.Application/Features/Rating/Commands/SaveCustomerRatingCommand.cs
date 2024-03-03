using AutoMapper;
using MainMikitan.Database.Features.Rating.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Models.Rating;
using MainMikitan.Domain.Requests.Rating;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Application.Features.Rating.Commands;

public class SaveCustomerRatingCommand(int restaurantId, int userId, SaveCustomerRatingRequest request) : ICommand<bool>
{
    public int RestaurantId { get; set; } = restaurantId;
    public int UserId { get; set; } = userId;
    public SaveCustomerRatingRequest Request { get; set; } = request;
}

public class SaveCustomerRatingCommandHandler(ICustomerRatingCommandRepository customerRatingCommandRepository,
    IMapper mapper)
    : ResponseMaker, ICommandHandler<SaveCustomerRatingCommand, bool>
{
    public async Task<ResponseModel<bool>> Handle(SaveCustomerRatingCommand command,CancellationToken cancellationToken)
    {
        var customerRatingEntity = mapper.Map<CustomerRatingEntity>(command.Request);
        customerRatingEntity.UserId = command.UserId;
        customerRatingEntity.RestaurantId = command.RestaurantId;
        customerRatingEntity.CreatedAt = DateTime.Now;
        
        await customerRatingCommandRepository.SaveRating(customerRatingEntity);
        return await customerRatingCommandRepository.SaveChangesAsync() ? Success() : Fail(ErrorResponseType.Rating.RatingSaveFail);
    }
}