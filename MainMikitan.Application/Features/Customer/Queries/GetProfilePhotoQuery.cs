using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Templates;
using MediatR;

namespace MainMikitan.Application.Features.Customer.Queries;

public class GetProfilePhotoQuery : IQuery<string>
{
    public int CustomerId { get; set; }

    public GetProfilePhotoQuery(int customerId)
    {
        CustomerId = customerId;
    }
}

public class GetProfilePhotoQueryHandler : IQueryHandler<GetProfilePhotoQuery, string>
{
    public GetProfilePhotoQueryHandler()
    {
            
    }
    
    public Task<ResponseModel<string>> Handle(GetProfilePhotoQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
