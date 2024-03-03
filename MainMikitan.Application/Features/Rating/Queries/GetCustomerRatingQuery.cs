using MainMikitan.Database.Features.Rating.Interface;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Responses.Rating;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Application.Features.Rating.Queries;

public class GetCustomerRatingQuery(int customerId) : IQuery<GetCustomerRatingResponse>
{
    public int CustomerId { get; set; } = customerId;
}

public class GetCustomerRatingQueryHandler(ICustomerRatingRepository customerRatingRepository)
    : ResponseMaker<GetCustomerRatingResponse>,
        IQueryHandler<GetCustomerRatingQuery, GetCustomerRatingResponse>
{
    public async Task<ResponseModel<GetCustomerRatingResponse>> Handle(GetCustomerRatingQuery query,
        CancellationToken cancellationToken)
    {
        var ratings = await customerRatingRepository.GetCustomerRatings(query.CustomerId, cancellationToken);

        return Success(new GetCustomerRatingResponse
        {
            CustomerId = query.CustomerId,
            Rating = ratings
                .Where(r => r.CreatedAt > DateTime.Now.AddYears(-1))
                .Average(ra => ra.Rating)
        });
    }
}