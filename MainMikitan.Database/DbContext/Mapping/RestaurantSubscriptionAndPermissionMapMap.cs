using MainMikitan.Domain.Models.Restaurant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainMikitan.Database.DbContext.Mapping;

public class RestaurantSubscriptionAndPermissionMapMap : IEntityTypeConfiguration<RestaurantSubscriptionAndPermissionMapEntity>
{
    public void Configure(EntityTypeBuilder<RestaurantSubscriptionAndPermissionMapEntity> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.SubscriptionTypeId).IsRequired();
        builder.Property(b => b.PermissionId).IsRequired();
        builder.Property(b => b.CreatedAt).IsRequired();
    }
}