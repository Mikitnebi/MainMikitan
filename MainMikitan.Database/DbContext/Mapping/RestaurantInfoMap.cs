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
        builder.Property(b => b.BusinessTypeId).IsRequired();
        builder.Property(b => b.HasCoupe).IsRequired();
        builder.Property(b => b.HasTerrace).IsRequired();
        builder.Property(b => b.PriceTypeId).IsRequired();
        builder.Property(b => b.HallStartHour).IsRequired();
        builder.Property(b => b.HallEndHour).IsRequired();
        builder.Property(b => b.HallStartMinute).IsRequired();
        builder.Property(b => b.HallEndMinute).IsRequired();
        builder.Property(b => b.KitchenStartHour).IsRequired();
        builder.Property(b => b.KitchenEndHour).IsRequired();
        builder.Property(b => b.KitchenStartMinute).IsRequired();
        builder.Property(b => b.KitchenEndMinute).IsRequired();
        builder.Property(b => b.MusicStartHour).IsRequired();
        builder.Property(b => b.MusicEndHour).IsRequired();
        builder.Property(b => b.MusicStartMinute).IsRequired();
        builder.Property(b => b.MusicEndMinute).IsRequired();
        builder.Property(b => b.ForCorporateEvents).IsRequired();
        builder.Property(b => b.DescriptionGeo);
        builder.Property(b => b.DescriptionEng);
        builder.Property(b => b.ActiveStatusId).IsRequired();
        builder.Property(b => b.UpdateUserId).IsRequired();
        builder.Property(b => b.UpdateAt);
        builder.Property(b => b.CreateAt).IsRequired();
    }
}