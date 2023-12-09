using MainMikitan.Domain.Models.Customer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainMikitan.Database.DbContext.Mapping;

public class CustomerInterestMap : IEntityTypeConfiguration<CustomerInterestEntity>
{
    public void Configure(EntityTypeBuilder<CustomerInterestEntity> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.CustomerId).IsRequired();
        builder.Property(b => b.InterestId).IsRequired();
        builder.Property(b => b.CreatedAt).IsRequired();
    }
}