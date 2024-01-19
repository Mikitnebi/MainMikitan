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
        IS3Adapter s3Adapter)
        : ResponseMaker<GetCustomerInfoResponse>, IQueryHandler<GetCustomerInfoQuery, GetCustomerInfoResponse>
    {
        public async Task<ResponseModel<GetCustomerInfoResponse>> Handle(GetCustomerInfoQuery query, CancellationToken cancellationToken) {
            var customerId = query.CustomerId;
            try
            {
                var customerInfo = await customerInfoRepository.Get(customerId);
                if (customerInfo is null)
                    return Fail(ErrorType.CustomerInfo.NotGetInfo);
                GetImageResponse? customerImageUrlResponse = null;
                try
                {
                    customerImageUrlResponse = await s3Adapter.GetCustomerProfileImage(customerId);
                }
                catch (MainMikitanException ex)
                {
                    return Fail(ErrorType.S3.ImageNotCreatedOrUpdated);
                }

                return Success(new GetCustomerInfoResponse
                {
                    BirthDate = customerInfo.BirthDate,
                    FullName = customerInfo.FullName,
                    NationalityId = customerInfo.NationalityId,
                    GenderId = customerInfo.GenderId,
                    ImageData = customerImageUrlResponse!
                });
            } catch (Exception ex)
            {
                return Unexpected(ex.Message);
            }
        }
    }
    