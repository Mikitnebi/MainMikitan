using MainMikitan.Domain.Models.Customer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainMikitan.Database.DbContext.Mapping;

public class CustomerInfoMap : IEntityTypeConfiguration<CustomerInfoEntity>
{
    public void Configure(EntityTypeBuilder<CustomerInfoEntity> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.FullName).IsRequired();
        builder.Property(b => b.CustomerId).IsRequired();
        builder.Property(b => b.BirthDate).IsRequired();
        builder.Property(b => b.GenderId).IsRequired();
        builder.Property(b => b.NationalityId).IsRequired();
        builder.Property(b => b.CreatedAt).IsRequired();
    }
}