using MainMikitan.Domain.Models.Restaurant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainMikitan.Database.DbContext.Mapping;

public class RestaurantInfoMap : IEntityTypeConfiguration<RestaurantInfoEntity>
{
    public void Configure(EntityTypeBuilder<RestaurantInfoEntity> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.RestaurantId).IsRequired();
        builder.Property(b => b.LocationX).IsRequired();
        builder.Property(b => b.LocationY).IsRequired();
        builder.Property(b => b.Address).IsRequired();
        builder.Property(b => b.AddressEng).IsRequired();
        builder.Property(b => b.BusinessTypeId).IsRequired();
        builder.Property(b => b.HasCoupe).IsRequired();
        builder.Property(b => b.HasTerrace).IsRequired();
        builder.Property(b => b.PriceTypeId).IsRequired();
        builder.Property(b => b.HallStartTime).IsRequired();
        builder.Property(b => b.HallEndTime).IsRequired();
        builder.Property(b => b.KitchenStartTime).IsRequired();
        builder.Property(b => b.KitchenEndTime).IsRequired();
        builder.Property(b => b.MusicStartTime).IsRequired();
        builder.Property(b => b.MusicEndTime).IsRequired();
        builder.Property(b => b.ForCorporateEvents).IsRequired();
        builder.Property(b => b.DescriptionGeo);
        builder.Property(b => b.DescriptionEng);
        builder.Property(b => b.UpdateUserId).IsRequired();
        builder.Property(b => b.UpdatedAt);
        builder.Property(b => b.CreatedAt).IsRequired();
    }
}