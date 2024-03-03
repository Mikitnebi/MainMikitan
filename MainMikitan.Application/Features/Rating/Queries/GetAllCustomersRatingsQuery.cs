using MainMikitan.Database.Features.Rating.Interface;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Responses.Rating;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Application.Features.Rating.Queries;

public class GetAllCustomersRatingsQuery : IQuery<List<GetCustomerRatingResponse>>
{
}

public class GetAllCustomersRatingsQueryHandler(ICustomerRatingRepository customerRatingRepository)
    : ResponseMaker<List<GetCustomerRatingResponse>>,
        IQueryHandler<GetAllCustomersRatingsQuery, List<GetCustomerRatingResponse>>
{
    public async Task<ResponseModel<List<GetCustomerRatingResponse>>> Handle(GetAllCustomersRatingsQuery query,
        CancellationToken cancellationToken)
    {
        var ratings = await customerRatingRepository.GetAllActiveCustomersRatings(cancellationToken);

        List<GetCustomerRatingResponse> response = [];
        
        response.AddRange(ratings.Select(activeRating => new GetCustomerRatingResponse
        {
            CustomerId = activeRating.UserId,
            Rating = ratings.Where(r => r.UserId == activeRating.UserId)
                .Average(ra => ra.Rating)
        }));

        return Success(response);
    }
}