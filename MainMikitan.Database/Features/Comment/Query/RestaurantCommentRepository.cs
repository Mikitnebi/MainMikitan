using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.Comment.Interface;
using MainMikitan.Domain.Models.Comments;
using Microsoft.EntityFrameworkCore;

namespace MainMikitan.Database.Features.Comment.Query;

public class RestaurantCommentRepository(MikDbContext db) : IRestaurantCommentRepository
{
    public async Task<List<RestaurantCommentEntity>> GetRestaurantComments(int restaurantId, CancellationToken cancellationToken)
    {
        return await db.RestaurantComments.Where(rc => rc.RestaurantId == restaurantId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}