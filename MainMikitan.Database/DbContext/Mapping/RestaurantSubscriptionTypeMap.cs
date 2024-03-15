using MainMikitan.Domain.Models.Restaurant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainMikitan.Database.DbContext.Mapping;

public class RestaurantSubscriptionTypeMap: IEntityTypeConfiguration<RestaurantSubscriptionTypeEntity>
{
    public void Configure(EntityTypeBuilder<RestaurantSubscriptionTypeEntity> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.SubscriptionTypeName).IsRequired();
        builder.Property(b => b.PermissionListId).IsRequired();
        builder.Property(b => b.CreatedAt).IsRequired();
    }
}