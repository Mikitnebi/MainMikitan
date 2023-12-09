using MainMikitan.Domain.Models.Customer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainMikitan.Database.DbContext.Mapping;

public class CustomersMap : IEntityTypeConfiguration<CustomerEntity>
{
    public void Configure(EntityTypeBuilder<CustomerEntity> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.FullName).IsRequired();
        builder.Property(b => b.EmailAddress).IsRequired();
        builder.Property(b => b.EmailConfirmation).IsRequired();
        builder.Property(b => b.HashPassWord).IsRequired();
        builder.Property(b => b.StatusId).IsRequired();
        builder.Property(b => b.CreatedAt).IsRequired();
    }
}