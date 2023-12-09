using MainMikitan.Domain.Models.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainMikitan.Database.DbContext.Mapping;

public class OtpLogIntroMap : IEntityTypeConfiguration<OtpLogIntroEntity>
{
    public void Configure(EntityTypeBuilder<OtpLogIntroEntity> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Otp);
        builder.Property(b => b.EmailAddress);
        builder.Property(b => b.MobileNumber);
        builder.Property(b => b.StatusId);
        builder.Property(b => b.ValidationTime);
        builder.Property(b => b.NumberOfTrials);
        builder.Property(b => b.NumberOfTrialsIsRequired);
        builder.Property(b => b.UserTypeId);
        builder.Property(b => b.CreatedAt);
    }
}