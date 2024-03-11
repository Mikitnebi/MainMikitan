using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.LegalEntity.Interface;
using MainMikitan.Domain.Models.Company;
using MainMikitan.Domain.Models.Restaurant;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Database.Features.LegalEntity.Command
{
    public class LegalEntityCommandRepository(MikDbContext db) : ILegalEntityCommandRepository
    {
        public async Task<bool> Add(LegalEntityEntity entity, CancellationToken cancellationToken = default)
        {
            var addResponse = await db.LegalEntity.AddAsync(entity, cancellationToken);
            return addResponse.State == EntityState.Added;
        }

        public async Task<bool> SaveChanges(CancellationToken cancellationToken = default)
        {
            var saveResponse = await db.SaveChangesAsync(cancellationToken);
            return saveResponse > 0;
        }
    }
}
