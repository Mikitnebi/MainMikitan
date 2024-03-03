using MainMikitan.Database.Features.Rating.Interface;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Responses.Rating;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Application.Features.Rating.Queries;

public class GetAllRestaurantsRatingQuery : IQuery<List<GetRestaurantRatingResponse>>
{
}

public class GetAllRestaurantsRatingQueryHandler(IRestaurantRatingRepository customerRatingRepository)
    : ResponseMaker<List<GetRestaurantRatingResponse>>,
        IQueryHandler<GetAllRestaurantsRatingQuery, List<GetRestaurantRatingResponse>>
{
    public async Task<ResponseModel<List<GetRestaurantRatingResponse>>> Handle(GetAllRestaurantsRatingQuery query,
        CancellationToken cancellationToken)
    {
        var ratings = await customerRatingRepository.GetAllActiveCustomersRatings(cancellationToken);

        List<GetRestaurantRatingResponse> response = [];
        
        response.AddRange(ratings.Select(activeRating => new GetRestaurantRatingResponse
        {
            RestaurantId = activeRating.RestaurantId,
            Rating = ratings.Where(r => r.UserId == activeRating.UserId)
                .Average(ra => ra.Rating)
        }));

        return Success(response);
    }
}