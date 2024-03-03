using MainMikitan.Domain.Models.Reservation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainMikitan.Database.DbContext.Mapping;

public class ReservationMap : IEntityTypeConfiguration<ReservationEntity>
{
    public void Configure(EntityTypeBuilder<ReservationEntity> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.UserId);
        builder.Property(b => b.RestaurantId);
        builder.Property(b => b.IsCompany);
        builder.Property(b => b.Value);
        builder.Property(b => b.TableId);
        builder.Property(b => b.ReservedManuId);
        builder.Property(b => b.Date);
        builder.Property(b => b.Time);
        builder.Property(b => b.CreatedAt);
        builder.Property(b => b.Comment);
        builder.Property(b => b.GuestArrived);
        builder.Property(b => b.IsCanceled);
        builder.Property(b => b.IsCompleted);
    }
}