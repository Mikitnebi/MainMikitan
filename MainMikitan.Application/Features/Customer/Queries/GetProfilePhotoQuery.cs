using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Responses.S3Response;
using MainMikitan.Domain.Templates;
using MainMikitan.ExternalServicesAdapter.S3ServiceAdapter;
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
    
    public Task<ResponseModel<GetImageResponse>> Handle(GetProfilePhotoQuery request, CancellationToken cancellationToken)
    {
        var response = new ResponseModel<GetImageResponse>();
        try
        {
            //var customer
            return null;
        }
        catch (Exception ex) {
            response.ErrorType = ErrorType.UnExpectedException;
            response.ErrorMessage = ex.Message;
            return null;
        }
    }
}
