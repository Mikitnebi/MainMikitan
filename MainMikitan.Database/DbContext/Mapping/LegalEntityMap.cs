using MainMikitan.Domain.Models.Company;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Database.DbContext.Mapping
{
    public class LegalEntityMap : IEntityTypeConfiguration<LegalEntityEntity>
    {
        public void Configure(EntityTypeBuilder<LegalEntityEntity> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.BusinessNameEng).IsRequired();
            builder.Property(b => b.BusinessNameGeo).IsRequired();
            builder.Property(b => b.Email).IsRequired();
            builder.Property(b => b.CreatedAt).IsRequired();
            builder.Property(b => b.LegalEntityRepresentativeNameEng).IsRequired();
            builder.Property(b => b.LegalEntityRepresentativeNameGeo).IsRequired();
            builder.Property(b => b.PhoneNumber).IsRequired();
            builder.Property(b => b.StatusId).IsRequired();
            builder.Property(b => b.UsernameHash).IsRequired();
            builder.Property(b => b.PasswordHash).IsRequired();
        }
    }
}
