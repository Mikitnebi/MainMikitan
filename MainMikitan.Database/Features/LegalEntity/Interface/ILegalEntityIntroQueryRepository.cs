using MainMikitan.Domain.Models.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Database.Features.LegalEntity.Interface
{
    public interface ILegalEntityIntroQueryRepository
    {
        public Task<LegalEntityIntroEntity?> GetNonVerifiedByEmail(string email, CancellationToken cancellationToken = default);

        public Task<LegalEntityIntroEntity?> GetVerifiedByEmail(string email, CancellationToken cancellationToken = default);
    }
}
