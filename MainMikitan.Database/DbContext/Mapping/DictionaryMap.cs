using MainMikitan.Domain.Models.Common;
using MainMikitan.Domain.Models.ListOfValue;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainMikitan.Database.DbContext.Mapping;

public class DictionaryMap : IEntityTypeConfiguration<DictionaryEntity>
{
    public void Configure(EntityTypeBuilder<DictionaryEntity> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.SectorId).IsRequired();
        builder.Property(b => b.NameGeo).IsRequired();
        builder.Property(b => b.NameEng).IsRequired();
    }
}