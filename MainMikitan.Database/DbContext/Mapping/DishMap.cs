using MainMikitan.Domain.Models.Menu;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainMikitan.Database.DbContext.Mapping;

public class DishMap : IEntityTypeConfiguration<DishEntity>
{
    public void Configure(EntityTypeBuilder<DishEntity> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.CategoryDishId).IsRequired();
        builder.Property(b => b.RestaurantId).IsRequired();
        builder.Property(b => b.IsVerified).IsRequired();
        builder.Property(b => b.IsDeleted).IsRequired();
        builder.Property(b => b.IsActive).IsRequired();
        builder.Property(b => b.HasDifferentPicture).IsRequired();
        builder.Property(b => b.CreateAt).IsRequired();
        builder.Property(b => b.CreateUserId).IsRequired();
    }
}