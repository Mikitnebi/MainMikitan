using MainMikitan.Domain.Models.Restaurant.TableManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainMikitan.Database.DbContext.Mapping;

public class TableEnvironmentMap : IEntityTypeConfiguration<TableEnvironmentEntity>
{
    public void Configure(EntityTypeBuilder<TableEnvironmentEntity> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.TableId).IsRequired();
        builder.Property(b => b.EnvironmentId).IsRequired();
        builder.Property(b => b.CreatedAt).IsRequired();
    }
}