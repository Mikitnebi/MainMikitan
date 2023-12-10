using MainMikitan.Domain.Models.Common;
using MainMikitan.Domain.Models.Customer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainMikitan.Database.DbContext.Mapping;

public class CategoryMap : IEntityTypeConfiguration<CategoryEntity>
{
    public void Configure(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.StatusId).IsRequired();
        builder.Property(b => b.NameEng).IsRequired();
        builder.Property(b => b.NameGeo).IsRequired();
        builder.Property(b => b.CreatedAt).IsRequired();
    }
}