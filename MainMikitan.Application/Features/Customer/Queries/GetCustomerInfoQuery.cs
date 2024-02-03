using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.Customer;
using MainMikitan.Domain.Responses.S3Response;
using MainMikitan.Domain.Templates;
using MainMikitan.ExternalServicesAdapter.S3ServiceAdapter;
using MainMikitan.InternalServiceAdapterService.Exceptions;

namespace MainMikitan.Application.Features.Customer.Queries;

public class GetCustomerInfoQuery(int customerId) : IQuery<GetCustomerInfoResponse>
{
        public int CustomerId { get; set; } = customerId;
}
    public class GetCustomerInfoQueryHandler(
        ICustomerInfoRepository customerInfoRepository,
        ICustomerQueryRepository customerQueryRepository,
        IS3Adapter s3Adapter)
        : ResponseMaker<GetCustomerInfoResponse>, IQueryHandler<GetCustomerInfoQuery, GetCustomerInfoResponse>
    {
        public async Task<ResponseModel<GetCustomerInfoResponse>> Handle(GetCustomerInfoQuery query, CancellationToken cancellationToken) {
            var customerId = query.CustomerId;
            try
            {
                var customerInfo = await customerInfoRepository.Get(customerId, cancellationToken);
                var customer = await customerQueryRepository.GetById(query.CustomerId, cancellationToken);
                if (customerInfo is null || customer is null)
                    return Fail(ErrorType.CustomerInfo.NotGetInfo);

                return Success(new GetCustomerInfoResponse
                {
                    BirthDate = customerInfo.BirthDate,
                    NationalityId = customerInfo.NationalityId,
                    GenderId = customerInfo.GenderId,
                    FullName = customer.FullName
                });
            } catch (Exception ex)
            {
                return Unexpected(ex);
            }
        }
    }
    