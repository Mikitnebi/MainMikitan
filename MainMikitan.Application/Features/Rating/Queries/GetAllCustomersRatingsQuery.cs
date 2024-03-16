using MainMikitan.Application.Services.Permission;
using MainMikitan.Database.Features.Rating.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Responses.Rating;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Application.Features.Rating.Queries;

public class GetAllCustomersRatingsQuery(int restaurantId, int userId, string userRole, IEnumerable<int> permissionIds) : IQuery<List<GetCustomerRatingResponse>>
{
    public IEnumerable<int> PermissionIds { get; set; } = permissionIds;
    public string UserRole { get; set; } = userRole;
    public int RestaurantId { get; set; } = restaurantId;
    public int UserId { get; set; } = userId;
}

public class GetAllCustomersRatingsQueryHandler(ICustomerRatingRepository customerRatingRepository,
    IPermissionService permissionService)
    : ResponseMaker<List<GetCustomerRatingResponse>>,
        IQueryHandler<GetAllCustomersRatingsQuery, List<GetCustomerRatingResponse>>
{
    public async Task<ResponseModel<List<GetCustomerRatingResponse>>> Handle(GetAllCustomersRatingsQuery query,
        CancellationToken cancellationToken)
    {
        if (!await permissionService.Check(query.UserId, query.PermissionIds, query.UserRole,
                cancellationToken, query.RestaurantId, 2))
            return Fail(ErrorResponseType.Staff.StaffForbiddenPermission);
        
        var ratings = await customerRatingRepository.GetAllActiveCustomersRatings(cancellationToken);

        List<GetCustomerRatingResponse> response = [];
        
        response.AddRange(ratings.Select(activeRating => new GetCustomerRatingResponse
        {
            CustomerId = activeRating.UserId,
            Rating = ratings.Where(r => r.UserId == activeRating.UserId)
                .Average(ra => ra.Rating)
        }));

        return Success(response);
    }
}