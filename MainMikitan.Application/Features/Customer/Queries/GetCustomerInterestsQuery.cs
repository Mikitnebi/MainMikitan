using MainMikitan.Database.Features.Customer.Command;
using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.Customer;
using MediatR;
using NPOI.SS.Formula.Functions;

namespace MainMikitan.Application.Features.Customer.Queries;

public class GetCustomerInterestsQuery : IRequest<ResponseModel<GetCustomerInterestsResponse>> {
        public int CustomerId { get; set; }
        public GetCustomerInterestsQuery(int customerId) {
            CustomerId = customerId;
        }
    }

public class
    GetCustomerInterestsQueryHandler : IRequestHandler<GetCustomerInterestsQuery,
        ResponseModel<GetCustomerInterestsResponse>>
{
    private readonly ICustomerInterestRepository _customerInterestRepository;

    public GetCustomerInterestsQueryHandler(
        ICustomerInterestRepository customerInterestRepository
    )
    {
        _customerInterestRepository = customerInterestRepository;
    }

    public async Task<ResponseModel<GetCustomerInterestsResponse>> Handle(GetCustomerInterestsQuery query,
        CancellationToken cancellationToken)
    {
        var response = new ResponseModel<GetCustomerInterestsResponse>();
        var customerId = query.CustomerId;
        try
        {
            var customerInterests = await _customerInterestRepository.Get(customerId);
            response.Result = new GetCustomerInterestsResponse
            {
                InterestsIds = customerInterests.Select(t => t.InterestId).ToList()
            };
            return response;
        }
        catch (Exception ex)
        {
            response.ErrorType = ErrorType.UnExpectedException;
            response.ErrorMessage = ex.Message;
            return response;
        }
    }
}