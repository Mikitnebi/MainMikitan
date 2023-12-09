using MainMikitan.Domain.Models.Restaurant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainMikitan.Database.DbContext.Mapping;

public class RestaurantEnvironmentInfoMap : IEntityTypeConfiguration<RestaurantEnvironmentInfoEntity>
{
    public void Configure(EntityTypeBuilder<RestaurantEnvironmentInfoEntity> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.RestaurantId).IsRequired();
        builder.Property(b => b.EnvironmentId).IsRequired();
        builder.Property(b => b.IsActive).IsRequired();
        builder.Property(b => b.CreatedAt).IsRequired();
    }
}

