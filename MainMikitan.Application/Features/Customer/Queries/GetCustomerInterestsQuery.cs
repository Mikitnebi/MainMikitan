using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.Customer;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Application.Features.Customer.Queries;

public class GetCustomerInterestsQuery(int customerId) : IQuery<GetCustomerInterestsResponse>
{
        public int CustomerId { get; set; } = customerId;
}

public class  GetCustomerInterestsQueryHandler(ICustomerInterestQueryRepository customerInterestQueryRepository)
    : ResponseMaker<GetCustomerInterestsResponse>,IQueryHandler<GetCustomerInterestsQuery, GetCustomerInterestsResponse>
{
    public async Task<ResponseModel<GetCustomerInterestsResponse>> Handle(GetCustomerInterestsQuery query,
        CancellationToken cancellationToken)
    {
        var customerId = query.CustomerId;
        try
        {
            var customerInterests = await customerInterestQueryRepository.GetByCustomerId(customerId, cancellationToken);
            return Success(new GetCustomerInterestsResponse
            {
                InterestsIds = customerInterests.Select(t => t.InterestId).ToList()
            });
        }
        catch (Exception ex)
        {
            return Unexpected(ex);
        }
    }
}