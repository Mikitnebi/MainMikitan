using MainMikitan.Domain.Models.Restaurant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainMikitan.Database.DbContext.Mapping;

public class RestaurantIntroMap : IEntityTypeConfiguration<RestaurantIntroEntity>
{
    public void Configure(EntityTypeBuilder<RestaurantIntroEntity> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.StatusId).IsRequired();
        builder.Property(b => b.EmailAddress).IsRequired();
        builder.Property(b => b.EmailConfirmation);
        builder.Property(b => b.RestaurantOtpVerificationId);
        builder.Property(b => b.BusinessNameGeo).IsRequired();
        builder.Property(b => b.BusinessNameEng).IsRequired();
        builder.Property(b => b.PhoneNumber).IsRequired();
        builder.Property(b => b.RegionId).IsRequired();
        builder.Property(b => b.ParentId);
        builder.Property(b => b.ParentConfirmation);
        builder.Property(b => b.CreatedAt).IsRequired();
    }
}