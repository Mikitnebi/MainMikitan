using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Responses.S3Response;
using MainMikitan.Domain.Templates;
using MainMikitan.ExternalServicesAdapter.S3ServiceAdapter;
using MainMikitan.InternalServiceAdapterService.Exceptions;
using MediatR;

namespace MainMikitan.Application.Features.Customer.Queries;

public class GetProfilePhotoQuery : IQuery<GetImageResponse>
{
    public int CustomerId { get; set; }

    public GetProfilePhotoQuery(int customerId)
    {
        CustomerId = customerId;
    }
}

public class GetProfilePhotoQueryHandler : IQueryHandler<GetProfilePhotoQuery, GetImageResponse>
{
    private readonly IS3Adapter _s3Adapter;
    public GetProfilePhotoQueryHandler(IS3Adapter s3Adapter)
    {
        _s3Adapter = s3Adapter;
    }
    
    public async Task<ResponseModel<GetImageResponse>> Handle(GetProfilePhotoQuery request, CancellationToken cancellationToken)
    {
        var response = new ResponseModel<GetImageResponse>();
        try
        {
            try
            {
                var customerImage = await _s3Adapter.GetCustomerProfileImage(request.CustomerId);
                response.Result = customerImage;
                return response;
            }
            catch (MainMikitanException ex)
            {
                response.ErrorType = ErrorType.S3.ImageNotCreatedOrUpdated;
                response.ErrorMessage = ex.Message;
                return response;
            }
        }
        catch (Exception ex) {
            response.ErrorType = ErrorType.UnExpectedException;
            response.ErrorMessage = ex.Message;
            return null;
        }
    }
}
