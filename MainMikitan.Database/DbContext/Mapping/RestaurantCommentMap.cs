using MainMikitan.Domain.Models.Comments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainMikitan.Database.DbContext.Mapping;

public class RestaurantCommentMap : IEntityTypeConfiguration<RestaurantCommentEntity>
{
    public void Configure(EntityTypeBuilder<RestaurantCommentEntity> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.UserId);
        builder.Property(b => b.RestaurantId);
        builder.Property(b => b.ReservationId);
        builder.Property(b => b.CommentTagId);
        builder.Property(b => b.Comment);
        builder.Property(b => b.CreatedAt);
    }
}