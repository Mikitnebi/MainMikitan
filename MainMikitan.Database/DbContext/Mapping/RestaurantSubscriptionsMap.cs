using MainMikitan.Domain.Models.Restaurant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainMikitan.Database.DbContext.Mapping;

public class RestaurantSubscriptionsMap: IEntityTypeConfiguration<RestaurantSubscriptionsEntity>
{
    public void Configure(EntityTypeBuilder<RestaurantSubscriptionsEntity> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.RestaurantId).IsRequired();
        builder.Property(b => b.RestaurantSubscriptionTypeId).IsRequired();
        builder.Property(b => b.SubscriptionActivationDate).IsRequired();
        builder.Property(b => b.SubscriptionDeactivationDate).IsRequired();
        builder.Property(b => b.CreatedAt).IsRequired();
    }
}