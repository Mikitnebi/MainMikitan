using MainMikitan.Domain.Models.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainMikitan.Database.DbContext.Mapping;

public class EventDetailsMap : IEntityTypeConfiguration<EventDetailsEntity>
{
    public void Configure(EntityTypeBuilder<EventDetailsEntity> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.EventId);
        builder.Property(b => b.NameGeo);
        builder.Property(b => b.NameEng);
        builder.Property(b => b.DescriptionGeo);
        builder.Property(b => b.DescriptionEng);
        builder.Property(b => b.MaxAttendances);
        builder.Property(b => b.NeedsRegistration);
        builder.Property(b => b.TakeManagersRegistrationAddress);
        builder.Property(b => b.EventAddressGeo);
        builder.Property(b => b.EventAddressEng);
        builder.Property(b => b.Price);
        builder.Property(b => b.HasMusician);
        builder.Property(b => b.Musician);
        builder.Property(b => b.HasPresenter);
        builder.Property(b => b.Presenter);
        builder.Property(b => b.HasDancer);
        builder.Property(b => b.Dancer);
        builder.Property(b => b.CreationDate);
    }
}