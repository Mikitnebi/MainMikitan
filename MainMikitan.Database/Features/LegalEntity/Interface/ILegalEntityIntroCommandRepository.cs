using MainMikitan.Database.Features.LegalEntity.Query;
using MainMikitan.Domain.Models.Company;
using MainMikitan.Domain.Requests.LegalEntityRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MainMikitan.Domain.Enums;

namespace MainMikitan.Database.Features.LegalEntity.Interface
{
    public interface ILegalEntityIntroCommandRepository
    {
        public Task<bool> UpdateStatus(string email, bool emailConfirmation, LegalEntityOtpVerificationId status, CancellationToken cancellationToken = default);

        public Task<bool> Create(LegalEntityIntroRegistrationRequest request, CancellationToken cancellationToken);


        public Task<bool> SaveChanges(CancellationToken cancellationToken = default);
       
    }
}
