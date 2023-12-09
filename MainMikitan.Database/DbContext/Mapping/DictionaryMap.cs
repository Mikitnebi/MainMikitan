using MainMikitan.Domain.Models.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainMikitan.Database.DbContext.Mapping;

public class DictionaryMap : IEntityTypeConfiguration<DictionaryEntity>
{
    public void Configure(EntityTypeBuilder<DictionaryEntity> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.SectorId).IsRequired();
        builder.Property(b => b.GeoName).IsRequired();
        builder.Property(b => b.EngName).IsRequired();
    }
}