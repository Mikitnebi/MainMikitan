using System.Linq.Expressions;
using AutoMapper;
using MainMikitan.Application.Services.Permission;
using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Responses.RestaurantResponses;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Application.Features.Restaurant.Info.Query;

public class ViewInfoQuery(int userId, string userRole, int restaurantId, IEnumerable<int> permissionIds) : IQuery<RestaurantInfoModel>
{
    public int UserId { get; set; } = userId;
    public string UserRole { get; set; } = userRole;
    public int RestaurantId { get; set; } = restaurantId;
    public IEnumerable<int> PermissionIds { get; set; } = permissionIds;
}

public class ViewInfoQueryHandler(
    IPermissionService permissionService,
    IRestaurantInfoQueryRepository restaurantInfoQueryRepository,
    IMapper mapper
) : ResponseMaker<RestaurantInfoModel>, IQueryHandler<ViewInfoQuery, RestaurantInfoModel>
{
    public async Task<ResponseModel<RestaurantInfoModel>> Handle(ViewInfoQuery query, CancellationToken cancellationToken)
    {
        try
        {
            if (!await permissionService.Check(query.UserId, query.PermissionIds, query.UserRole,
                    cancellationToken))
                return Fail(ErrorResponseType.Staff.StaffForbiddenPermission);
            var restaurantInfo =
                await restaurantInfoQueryRepository.GetByRestaurantId(query.RestaurantId, cancellationToken);
            var restaurantInfoResponse = mapper.Map<RestaurantInfoModel>(restaurantInfo);
            return Success(restaurantInfoResponse);
        }
        catch (Exception ex)
        {
            return Unexpected(ex);
        }
    }
}


