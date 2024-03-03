using MainMikitan.Domain.Models.Rating;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainMikitan.Database.DbContext.Mapping;

public class RestaurantRatingMap : IEntityTypeConfiguration<RestaurantRatingEntity>
{
    public void Configure(EntityTypeBuilder<RestaurantRatingEntity> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.CustomerId).IsRequired();
        builder.Property(b => b.UserId).IsRequired();
        builder.Property(b => b.RestaurantId).IsRequired();
        builder.Property(b => b.ReservationId).IsRequired();
        builder.Property(b => b.Rating).IsRequired();
        builder.Property(b => b.CreatedAt).IsRequired();
    }
}