using MainMikitan.Domain.Models.Company;
using MainMikitan.Domain.Models.Restaurant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Database.DbContext.Mapping
{
    public class LegalEntityIntroMap : IEntityTypeConfiguration<LegalEntityIntroEntity>
    {
        public void Configure(EntityTypeBuilder<LegalEntityIntroEntity> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.StatusId).IsRequired();
            builder.Property(b => b.EmailAddress).IsRequired();
            builder.Property(b => b.EmailConfirmation);
            builder.Property(b => b.BusinessNameGeo).IsRequired();
            builder.Property(b => b.BusinessNameEng).IsRequired();
            builder.Property(b => b.PhoneNumber).IsRequired();
            builder.Property(b => b.RegionId).IsRequired();
            builder.Property(b => b.CreatedAt).IsRequired();
        }
    }
}
