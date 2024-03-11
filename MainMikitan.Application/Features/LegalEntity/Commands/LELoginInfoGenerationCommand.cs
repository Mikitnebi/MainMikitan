using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.LegalEntityRequests;
using MainMikitan.Domain.Templates;
using NPOI.HSSF.Record;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MainMikitan.Domain.Templates;
using MainMikitan.Database.Features.Common.Email.Interfaces;
using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.ExternalServicesAdapter.Email;
using MainMikitan.InternalServiceAdapter.Hasher;
using MainMikitan.InternalServicesAdapter.Util;
using MainMikitan.Database.Features.LegalEntity.Interface;
using Amazon.Runtime;
using Amazon.Runtime.Internal;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Company;

namespace MainMikitan.Application.Features.LegalEntity.Commands
{
    public class LELoginInfoGenerationCommand(LegalEntityLoginInfoGenerationRequest request) : Domain.Templates.ICommand
    {
        public readonly LegalEntityLoginInfoGenerationRequest Request = request;
    }
    public class LELoginInfoGenerationCommandHandler(
        IEmailSenderQueryRepository emailSenderQueryRepository,
        ILegalEntityIntroQueryRepository legalEntityIntroQueryRepository,
        ILegalEntityCommandRepository legalEntityCommandRepository,
        IPasswordHasher passwordHasher,
        IEmailSenderService emailSenderService) : ResponseMaker, ICommandHandler<LELoginInfoGenerationCommand>
    {
        public async Task<ResponseModel<bool>> Handle(LELoginInfoGenerationCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseModel<bool>();
            try
            {
                var getLgalEntityIntro = await legalEntityIntroQueryRepository.GetVerifiedByEmail(request.Request.Email, cancellationToken);
                if (getLgalEntityIntro == null)
                    return Fail(ErrorResponseType.LegalEntityIntro.VerifiedLegalEntityNotFound);
                var generateUserName = UtilHelper.GenerateUserName();
                var generatePassword = UtilHelper.GeneratePassword();
                var legalEntity = new LegalEntityEntity
                {
                    Email = getLgalEntityIntro.EmailAddress,
                    BusinessNameEng = request.Request.BusinessNameEng,
                    BusinessNameGeo = request.Request.BusinessNameGeo,
                    LegalEntityRepresentativeNameEng = request.Request.LegalEntityRepresentativeNameEng,
                    LegalEntityRepresentativeNameGeo = request.Request.LegalEntityRepresentativeNameGeo,
                    CreatedAt = DateTime.Now,
                    PhoneNumber = request.Request.PhoneNumber,
                    UsernameHash = passwordHasher.Hash(generateUserName),
                    PasswordHash = passwordHasher.Hash(generatePassword),
                    StatusId = (int)Enums.LegalEntityStatusId.Verified
                };
                var addLegalEntity = await legalEntityCommandRepository.Add(legalEntity, cancellationToken);
                var saveEntity = await legalEntityCommandRepository.SaveChanges(cancellationToken);
                if (!addLegalEntity || !saveEntity)
                    return Fail(ErrorResponseType.LegalEntity.CouldNotCreateLegalEntity);
                var sendEmail = await emailSenderQueryRepository.GetEmailById((int)Enums.EmailType.LegalEntityGenerateAccount, cancellationToken);
                if (sendEmail == null)
                    return Fail(ErrorResponseType.LegalEntity.EmailTypeNotFound);
                var emailBuilder = new EmailSenderService.EmailBuilder();
                emailBuilder.AddReplacement("{Username}", generateUserName);
                emailBuilder.AddReplacement("{Password}", generatePassword);
                var emailSenderResult = await emailSenderService.SendEmailAsync(
                request.Request.Email!, emailBuilder,
                (int)Enums.EmailType.LegalEntityGenerateAccount);

                return !emailSenderResult ? Fail(ErrorResponseType.EmailSender.EmailNotSend) : Success();
            }
            catch (Exception ex) 
            { 
                return Unexpected(ex);
            }
        }
    }
}
