using MainMikitan.Database.Features.Common.Otp.Command;
using MainMikitan.Database.Features.Common.Otp.Interfaces;
using MainMikitan.Database.Features.LegalEntity.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Models.Setting;
using MainMikitan.Domain.Requests.LegalEntityRequests;
using MainMikitan.Domain.Templates;
using MainMikitan.ExternalServicesAdapter.Email;
using MainMikitan.InternalServiceAdapter.OtpGenerator;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MainMikitan.Domain.Enums;
using static MainMikitan.ExternalServicesAdapter.Email.EmailSenderService;

namespace MainMikitan.Application.Features.LegalEntity.Commands
{
    public class LegalEntityIntroRegistrationCommand(LegalEntityIntroRegistrationRequest command) : ICommand
    {
        public readonly LegalEntityIntroRegistrationRequest IntroRequest = command;
    }
    public class LegalEntityIntroRegistrationCommandHandler
        (ILegalEntityIntroCommandRepository legalEntityIntroCommandRepository,
        ILegalEntityIntroQueryRepository legalEntityIntroQueryRepository,
        IEmailSenderService emailSenderService, IOtpLogCommandRepository otpLogCommandRepository,
        IOptions<OtpOptions> otpConfig)
        : ResponseMaker, ICommandHandler<LegalEntityIntroRegistrationCommand>
    {
        public async Task<ResponseModel<bool>> Handle(LegalEntityIntroRegistrationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //VALIDATIONS MUST BE ADDED
                var email = request.IntroRequest.EmailAddress;
                var existingLegalEntity = await legalEntityIntroQueryRepository.GetVerifiedByEmail(email, cancellationToken);
                if (existingLegalEntity != null) {
                    return Fail(ErrorResponseType.LegalEntityIntro.LEWithThisEmailAlreadyExists);
                }

                var emailBuilder = new EmailBuilder();
                var otp = OtpGenerator.OtpGenerate();
                emailBuilder.AddReplacement("{OTP}", otp);
                //Email must be created in db
                //emailSenderQueryRepository
                var emailSenderResult = await emailSenderService.SendEmailAsync(email, emailBuilder, (int)EmailType.LegalEntityRegistrationEmail);
                var otpLogResult = await otpLogCommandRepository.Create(new Domain.Models.Common.OtpLogIntroEntity
                {
                    UserTypeId = (int)UserTypeId.LegalEntityIntro,
                    Otp = otp,
                    EmailAddress = email,
                    NumberOfTrialsIsRequired = false,
                    ValidationTime = otpConfig.Value.IntroValidationTime,
                    OperationId = (int)Enums.OtpOperationTypeId.LegalEntityIntroRegistration
                }, cancellationToken);
                var introRegistrationRequest = await legalEntityIntroCommandRepository.Create(request.IntroRequest, cancellationToken);
                if (!introRegistrationRequest) {
                    return Fail(ErrorResponseType.LegalEntityIntro.CouldNotCreateLegalEntityIntro);
                }
                return Success();
            }
            catch (Exception ex)
            {
                return Unexpected(ex);
            }
        }
    }

}
