using MainMikitan.Domain.Models.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainMikitan.Database.DbContext.Mapping;

public class EmailDictionaryMap : IEntityTypeConfiguration<EmailDictionaryEntity>
{
    public void Configure(EntityTypeBuilder<EmailDictionaryEntity> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Body);
        builder.Property(b => b.Subject);
        builder.Property(b => b.ReplacementQuantity);
    }
}