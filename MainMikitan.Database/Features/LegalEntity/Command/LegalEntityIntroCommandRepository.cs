using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.LegalEntity.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Company;
using MainMikitan.Domain.Requests.LegalEntityRequests;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MainMikitan.Domain.Enums;

namespace MainMikitan.Database.Features.LegalEntity.Command
{
    public class LegalEntityIntroCommandRepository(MikDbContext db, 
        ILegalEntityIntroQueryRepository legalEntityIntroQueryRepository) : ILegalEntityIntroCommandRepository
    {

        public async Task<bool> UpdateStatus(string email, bool emailConfirmation, LegalEntityOtpVerificationId status, CancellationToken cancellationToken = default) 
        {
            int confirmation = emailConfirmation == true ? 1 : 0;
            var updateStatusResponse = await legalEntityIntroQueryRepository.GetNonVerifiedByEmail(email);
            if (updateStatusResponse != null) {
                updateStatusResponse.StatusId = (int)status;
                updateStatusResponse.EmailConfirmation = emailConfirmation;
                return await SaveChanges(cancellationToken);
            }
            return false;
        }

        public async Task<bool> Create(LegalEntityIntroRegistrationRequest request, CancellationToken cancellationToken) 
        {
            var LegalEntityIntroAddResponse = await db.LegalEntityIntro.AddAsync(new LegalEntityIntroEntity {
                PhoneNumber = request.PhoneNumber,
                RegionId = request.RegionId,
                EmailAddress = request.EmailAddress,
                BusinessNameGeo = request.BusinessNameGeo,
                BusinessNameEng = request.BusinessNameEng,
                CreatedAt = DateTime.Now,
                StatusId = (int)Enums.LegalEntityStatusId.NonVerified,
                EmailConfirmation = false
            }, cancellationToken);
            return await SaveChanges(); 
        }

        public async Task<bool> SaveChanges(CancellationToken cancellationToken = default)
        {
            var response = await db.SaveChangesAsync(cancellationToken);
            return response > 0;
        }
    }
}
