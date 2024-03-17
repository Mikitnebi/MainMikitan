using MainMikitan.Application.Services.Permission;
using MainMikitan.Database.Features.Rating.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Responses.Rating;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Application.Features.Rating.Queries;

public class GetCustomerRatingQuery(int customerId, int restaurantId, int userId, string userRole, IEnumerable<int> permissionIds) : IQuery<GetCustomerRatingResponse>
{
    public int CustomerId { get; set; } = customerId;
    public IEnumerable<int> PermissionIds { get; set; } = permissionIds;
    public string UserRole { get; set; } = userRole;
    public int RestaurantId { get; set; } = restaurantId;
    public int UserId { get; set; } = userId;
}

public class GetCustomerRatingQueryHandler(ICustomerRatingRepository customerRatingRepository,
    IPermissionService permissionService)
    : ResponseMaker<GetCustomerRatingResponse>,
        IQueryHandler<GetCustomerRatingQuery, GetCustomerRatingResponse>
{
    public async Task<ResponseModel<GetCustomerRatingResponse>> Handle(GetCustomerRatingQuery query,
        CancellationToken cancellationToken)
    {
        if (!await permissionService.Check(query.UserId, query.PermissionIds, query.UserRole,
                cancellationToken, query.RestaurantId, 2))
            return Fail(ErrorResponseType.Staff.StaffForbiddenPermission);
        
        var ratings = await customerRatingRepository.GetCustomerRatings(query.CustomerId, cancellationToken);

        return Success(new GetCustomerRatingResponse
        {
            CustomerId = query.CustomerId,
            Rating = ratings
                .Where(r => r.CreatedAt > DateTime.Now.AddYears(-1))
                .Average(ra => ra.Rating)
        });
    }
}