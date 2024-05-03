using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.Customer;
using MainMikitan.Domain.Responses.S3Response;
using MainMikitan.Domain.Templates;
using MainMikitan.ExternalServicesAdapter.S3ServiceAdapter;
using MainMikitan.InternalServiceAdapterService.Exceptions;

namespace MainMikitan.Application.Features.Customer.Queries;

public class GetCustomerInfoQuery(int customerId) : IQuery<CustomerInfoResponse>
{
        public int CustomerId { get; set; } = customerId;
}
    public class GetCustomerInfoQueryHandler(
        ICustomerInfoQueryRepository customerInfoQueryRepository,
        ICustomerQueryRepository customerQueryRepository,
        IS3Adapter s3Adapter)
        : ResponseMaker<CustomerInfoResponse>, IQueryHandler<GetCustomerInfoQuery, CustomerInfoResponse>
    {
        public async Task<ResponseModel<CustomerInfoResponse>> Handle(GetCustomerInfoQuery query, CancellationToken cancellationToken) {
            var customerId = query.CustomerId;
            try
            {
                var customerInfo = await customerInfoQueryRepository.Get(customerId, cancellationToken);
                var customer = await customerQueryRepository.GetById(query.CustomerId, cancellationToken);
                if (customer is null)
                    return Fail(ErrorResponseType.CustomerInfo.NotGetInfo);
                var imageUrl = await s3Adapter.GetCustomerProfileImage(customerId);
            return Success(new CustomerInfoResponse
            {
                BirthDate = customerInfo?.BirthDate,
                NationalityId = customerInfo is null ? null : customerInfo.NationalityId,
                GenderId = customerInfo is null ? null : customerInfo.GenderId,
                FullName = customer.FullName,
                Email = customer.EmailAddress,
                ProfileImageUrl = imageUrl.Result is null ? null : imageUrl.Result.Url,
            }); 
            } catch (Exception ex)
            {
                return Unexpected(ex);
            }
        }
    }
    