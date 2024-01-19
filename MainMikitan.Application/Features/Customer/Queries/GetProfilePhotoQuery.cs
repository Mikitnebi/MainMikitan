using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Responses.S3Response;
using MainMikitan.Domain.Templates;
using MainMikitan.ExternalServicesAdapter.S3ServiceAdapter;
using MainMikitan.InternalServiceAdapterService.Exceptions;

namespace MainMikitan.Application.Features.Customer.Queries;

public class GetProfilePhotoQuery(int customerId) : IQuery<GetImageResponse>
{
    public int CustomerId { get; set; } = customerId;
}

public class GetProfilePhotoQueryHandler(IS3Adapter s3Adapter) : ResponseMaker<GetImageResponse>, IQueryHandler<GetProfilePhotoQuery, GetImageResponse>
{
    public async Task<ResponseModel<GetImageResponse>> Handle(GetProfilePhotoQuery request, CancellationToken cancellationToken)
    {
        var response = new ResponseModel<GetImageResponse>();
        try
        {
            try
            {
                var customerImage = await s3Adapter.GetCustomerProfileImage(request.CustomerId);
                response.Result = customerImage;
                return response;
            }
            catch (MainMikitanException ex)
            {
                return Fail(ErrorType.S3.ImageNotCreatedOrUpdated);
            }
        }
        catch (Exception ex)
        {
            return Unexpected(ex.Message);
        }
    }
}
