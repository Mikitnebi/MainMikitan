using MainMikitan.Domain.Models.Menu;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainMikitan.Database.DbContext.Mapping;

public class DishInfoMap : IEntityTypeConfiguration<DishInfoEntity>
{
    public void Configure(EntityTypeBuilder<DishInfoEntity> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.DishId).IsRequired();
        builder.Property(b => b.NameGeo).IsRequired();
        builder.Property(b => b.NameEng).IsRequired();
        builder.Property(b => b.IngredientsGeo).IsRequired();
        builder.Property(b => b.IngredientsEng).IsRequired();
        builder.Property(b => b.CreateAt).IsRequired();
    }
}