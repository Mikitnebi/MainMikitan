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

namespace MainMikitan.Database.Features.LegalEntity.Query
{
    public class LegalEntityIntroQueryRepository(MikDbContext db) : ILegalEntityIntroQueryRepository
    {
        public async Task<LegalEntityIntroEntity?> GetNonVerifiedByEmail(string email, CancellationToken cancellationToken = default)
        {
            return await db.LegalEntityIntro.Where(t =>
                t.EmailAddress == email && t.EmailConfirmation == false)
                .OrderByDescending(t => t.Id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<LegalEntityIntroEntity?> GetVerifiedByEmail(string email, CancellationToken cancellationToken = default)
        {
            return await db.LegalEntityIntro.FirstOrDefaultAsync(t =>
                t.EmailAddress == email && t.EmailConfirmation == true, cancellationToken: cancellationToken);
        }
    }
}
