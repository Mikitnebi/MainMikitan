using MainMikitan.Domain.Models.Restaurant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainMikitan.Database.DbContext.Mapping;

public class RestaurantMap : IEntityTypeConfiguration<RestaurantEntity>
{
    public void Configure(EntityTypeBuilder<RestaurantEntity> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.BusinessNameEng).IsRequired();
        builder.Property(b => b.BusinessNameGeo).IsRequired();
        builder.Property(b => b.EmailConfirmation).IsRequired();
        builder.Property(b => b.OfficialEmail).IsRequired();
        builder.Property(b => b.CreatedAt).IsRequired();
    }
}