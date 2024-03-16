using MainMikitan.Domain.Models.Log;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainMikitan.Database.DbContext.Mapping;

public class LoggerMap : IEntityTypeConfiguration<LoggerEntity>
{
    public void Configure(EntityTypeBuilder<LoggerEntity> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.MethodName).IsRequired();
        builder.Property(b => b.Exception);
        builder.Property(b => b.StackTrace);
        builder.Property(b => b.Data);
        builder.Property(b => b.ThrowTime).IsRequired();
    }
}