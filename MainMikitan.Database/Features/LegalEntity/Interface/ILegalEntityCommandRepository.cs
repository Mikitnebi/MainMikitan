using MainMikitan.Domain.Models.Company;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Database.Features.LegalEntity.Interface
{
    public interface ILegalEntityCommandRepository
    {
        public Task<bool> Add(LegalEntityEntity entity, CancellationToken cancellationToken = default);

        public Task<bool> SaveChanges(CancellationToken cancellationToken = default);
    }
}
