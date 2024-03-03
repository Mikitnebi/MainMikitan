using MainMikitan.Domain.Models.Comments;

namespace MainMikitan.Database.Features.Comment.Interface;

public interface IRestaurantCommentRepository
{
    public Task<List<RestaurantCommentEntity>> GetRestaurantComments(int restaurantId,
        CancellationToken cancellationToken);
}