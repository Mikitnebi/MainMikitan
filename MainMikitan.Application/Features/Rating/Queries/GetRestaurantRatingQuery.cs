using MainMikitan.Database.Features.Rating.Interface;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Responses.Rating;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Application.Features.Rating.Queries;

public class GetRestaurantRatingQuery(int restaurantId) : IQuery<GetRestaurantRatingResponse>
{
    public int RestaurantId { get; set; } = restaurantId;
}

public class GetRestaurantRatingQueryHandler(IRestaurantRatingRepository customerRatingRepository)
    : ResponseMaker<GetRestaurantRatingResponse>,
        IQueryHandler<GetRestaurantRatingQuery, GetRestaurantRatingResponse>
{
    public async Task<ResponseModel<GetRestaurantRatingResponse>> Handle(GetRestaurantRatingQuery query,
        CancellationToken cancellationToken)
    {
        var ratings = await customerRatingRepository.GetRestaurantRatings(query.RestaurantId, cancellationToken);

        return Success(new GetRestaurantRatingResponse
        {
            RestaurantId = query.RestaurantId,
            Rating = ratings
                .Where(r => r.CreatedAt > DateTime.Now.AddYears(-1))
                .Average(ra => ra.Rating)
        });
    }
}