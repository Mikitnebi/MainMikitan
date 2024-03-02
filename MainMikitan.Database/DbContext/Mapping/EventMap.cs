using MainMikitan.Domain.Models.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainMikitan.Database.DbContext.Mapping;

public class EventMap : IEntityTypeConfiguration<EventEntity>
{
    public void Configure(EntityTypeBuilder<EventEntity> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.RestaurantId);
        builder.Property(b => b.StartDate);
        builder.Property(b => b.EndDate);
        builder.Property(b => b.CreationDate);
    }
}