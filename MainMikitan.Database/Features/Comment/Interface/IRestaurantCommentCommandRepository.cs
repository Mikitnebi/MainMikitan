using MainMikitan.Domain.Models.Comments;

namespace MainMikitan.Database.Features.Comment.Interface;

public interface IRestaurantCommentCommandRepository
{
    public Task SaveRestaurantComment(RestaurantCommentEntity request);
    public Task<bool> SaveChangesAsync();
}