using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.Comment.Interface;
using MainMikitan.Domain.Models.Comments;

namespace MainMikitan.Database.Features.Comment.Command;

public class RestaurantCommentCommandRepository(MikDbContext db) : IRestaurantCommentCommandRepository
{
    public async Task SaveRestaurantComment(RestaurantCommentEntity request)
    {
        await db.RestaurantComments.AddAsync(request);
    }

    public async Task<bool> SaveChangesAsync()
    {
        var result = await db.SaveChangesAsync();

        return result > 0;
    }
}