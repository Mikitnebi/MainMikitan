using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.Customer;
using MainMikitan.Domain.Responses.S3Response;
using MainMikitan.Domain.Templates;
using MainMikitan.ExternalServicesAdapter.S3ServiceAdapter;
using MediatR;

namespace MainMikitan.Application.Features.Customer.Queries;

public class GetCustomerInfoQuery : IQuery<GetCustomerInfoResponse> {
        public int CustomerId { get; set; }
        public GetCustomerInfoQuery(int customerId) {
            CustomerId = customerId;
        }
    }
    public class GetCustomerInfoQueryHandler : IQueryHandler<GetCustomerInfoQuery, GetCustomerInfoResponse>
    {
        private readonly ICustomerInfoRepository _customerInfoRepository;
        private readonly IS3Adapter _s3Adapter;
        public GetCustomerInfoQueryHandler(
            ICustomerInfoRepository customerInfoRepository, 
            IS3Adapter s3Adapter)
        {
            _customerInfoRepository = customerInfoRepository;
            _s3Adapter = s3Adapter;
        }
        
        public async Task<ResponseModel<GetCustomerInfoResponse>> Handle(GetCustomerInfoQuery query, CancellationToken cancellationToken) {
            var response = new ResponseModel<GetCustomerInfoResponse>();
            var customerId = query.CustomerId;
            try
            {
                var customerInfo = await _customerInfoRepository.Get(customerId);
                if (customerInfo is null)
                {
                    response.ErrorType = ErrorType.CustomerInfo.NotGetInfo;
                    return response;
                }

                var customerImageUrlResponse = await _s3Adapter.GetCustomerProfileImage(customerId);
                if (customerImageUrlResponse.HasError)
                {
                    response.ErrorType = customerImageUrlResponse.ErrorType;
                    return response;
                }
                response.Result = new GetCustomerInfoResponse
                {
                    BirthDate = customerInfo.BirthDate,
                    FullName = customerInfo.FullName,
                    NationalityId = customerInfo.NationalityId,
                    GenderId = customerInfo.GenderId,
                    ImageData = customerImageUrlResponse.Result!
                };
                return response;
            } catch (Exception ex) {
                response.ErrorType = ErrorType.UnExpectedException;
                response.ErrorMessage = ex.Message;
                return response;
            }
        }
    }
    