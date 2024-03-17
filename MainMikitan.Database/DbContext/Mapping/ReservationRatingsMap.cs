using MainMikitan.Domain.Models.Rating;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainMikitan.Database.DbContext.Mapping;

public class ReservationRatingsMap : IEntityTypeConfiguration<ReservationRatingsEntity>
{
    public void Configure(EntityTypeBuilder<ReservationRatingsEntity> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.RestaurantId).IsRequired();
        builder.Property(b => b.UserId).IsRequired();
        builder.Property(b => b.ReservationId).IsRequired();
        builder.Property(b => b.OverallRestaurantRating).IsRequired();
        builder.Property(b => b.OverallAppRating);
        builder.Property(b => b.OverallDishRating);
        builder.Property(b => b.EnvironmentRating);
        builder.Property(b => b.ServiceRating);
        builder.Property(b => b.PriceRating);
        builder.Property(b => b.Comment);
        builder.Property(b => b.CreatedAt).IsRequired();
    }
}