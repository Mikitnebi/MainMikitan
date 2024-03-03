using MainMikitan.Database.Features.Comment.Interface;
using MainMikitan.Domain.Models.Comments;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Application.Features.Comment.Queries;

public class GetRestaurantCommentQuery(int restaurantId, int page, int size) : IQuery<List<RestaurantCommentEntity>>
{
    public int RestaurantId { get; set; } = restaurantId;
    public int Page { get; set; } = page;
    public int Size { get; set; } = size;
}

public class GetRestaurantCommentQueryHandler (IRestaurantCommentRepository restaurantCommentRepository)
    : ResponseMaker<List<RestaurantCommentEntity>>,
    IQueryHandler<GetRestaurantCommentQuery, List<RestaurantCommentEntity>>
{
    public async Task<ResponseModel<List<RestaurantCommentEntity>>> Handle(GetRestaurantCommentQuery query,
        CancellationToken cancellationToken)
    {
        var response = await restaurantCommentRepository.GetRestaurantComments(query.RestaurantId, cancellationToken);
        
        return Success(response.Count > 0 ? response.Skip((query.Page - 1) * query.Size).Take(query.Size).ToList() : response);
    }
}