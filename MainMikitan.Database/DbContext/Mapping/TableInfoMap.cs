using MainMikitan.Domain.Models.Restaurant.TableManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainMikitan.Database.DbContext.Mapping;

public class TableInfoMap : IEntityTypeConfiguration<TableInfoEntity>
{
    public void Configure(EntityTypeBuilder<TableInfoEntity> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.RestaurantId).IsRequired();
        builder.Property(b => b.TableNumber).IsRequired();
        builder.Property(b => b.MaxPlace).IsRequired();
        builder.Property(b => b.MinPlace).IsRequired();
        builder.Property(b => b.TableType).IsRequired();
        builder.Property(b => b.XCoordinate).IsRequired();
        builder.Property(b => b.YCoordinate).IsRequired();
        builder.Property(b => b.CreatedAt).IsRequired();
    }
}