using MainMikitan.Domain.Models.Reservation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainMikitan.Database.DbContext.Mapping;

public class ReservationMap : IEntityTypeConfiguration<ReservationEntity>
{
    public void Configure(EntityTypeBuilder<ReservationEntity> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.UserId).IsRequired();
        builder.Property(b => b.RestaurantId).IsRequired();
        builder.Property(b => b.IsCompany).IsRequired();
        builder.Property(b => b.Value).IsRequired();
        builder.Property(b => b.TableId).IsRequired();
        builder.Property(b => b.ReservedManuId).IsRequired();
        builder.Property(b => b.DateAt).IsRequired();
        builder.Property(b => b.CreatedAt).IsRequired();
        builder.Property(b => b.Comment).IsRequired();
        builder.Property(b => b.GuestArrived).IsRequired();
        builder.Property(b => b.IsCanceled).IsRequired();
        builder.Property(b => b.IsCompleted).IsRequired();
        builder.Property(b => b.GuestAmount).IsRequired();
    }
}