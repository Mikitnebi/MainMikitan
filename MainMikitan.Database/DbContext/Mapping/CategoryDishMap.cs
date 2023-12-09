using MainMikitan.Domain.Models.Menu;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainMikitan.Database.DbContext.Mapping;

public class CategoryDishMap : IEntityTypeConfiguration<CategoryDishEntity>
{
    public void Configure(EntityTypeBuilder<CategoryDishEntity> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.NameGeo).IsRequired();
        builder.Property(b => b.NameEng).IsRequired();
        builder.Property(b => b.CreatedAt).IsRequired();
    }
}