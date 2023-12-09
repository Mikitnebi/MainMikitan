using MainMikitan.Domain.Models.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainMikitan.Database.DbContext.Mapping;

public class EmailLogMap : IEntityTypeConfiguration<EmailLogEntity>
{
    public void Configure(EntityTypeBuilder<EmailLogEntity> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.EmailAddress).IsRequired();
        builder.Property(b => b.UserId).IsRequired();
        builder.Property(b => b.UserTypeId).IsRequired();
        builder.Property(b => b.EmailTypeId).IsRequired();
        builder.Property(b => b.Data).IsRequired();
        builder.Property(b => b.CreatedAt).IsRequired();
    }
}