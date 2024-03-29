using MainMikitan.Domain.Models.Restaurant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainMikitan.Database.DbContext.Mapping;

public class RestaurantStaffMap : IEntityTypeConfiguration<RestaurantStaffEntity>
{
    public void Configure(EntityTypeBuilder<RestaurantStaffEntity> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.FullNameGeo).IsRequired();
        builder.Property(b => b.FullNameEng).IsRequired();
        builder.Property(b => b.IsManager).IsRequired();
        builder.Property(b => b.PhoneNumber).IsRequired();
        builder.Property(b => b.Email).IsRequired();
        builder.Property(b => b.UserNameHash).IsRequired();
        builder.Property(b => b.PasswordHash).IsRequired();
        builder.Property(b => b.RestaurantId).IsRequired();
        builder.Property(b => b.IsActive).IsRequired();
        builder.Property(b => b.UpdateUserId);
        builder.Property(b => b.UpdateAt);
        builder.Property(b => b.CreateUserId).IsRequired();
        builder.Property(b => b.CreatedAt).IsRequired();
    }
}