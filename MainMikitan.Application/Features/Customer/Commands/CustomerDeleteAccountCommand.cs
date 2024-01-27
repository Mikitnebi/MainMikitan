using MainMikitan.Database.Features.Common.Otp.Interfaces;
using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Database.Features.Reservation.Interfaces;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Models.Setting;
using MainMikitan.Domain.Templates;
using MainMikitan.ExternalServicesAdapter.Email;
using MainMikitan.InternalServiceAdapter.OtpGenerator;
using Microsoft.Extensions.Options;

namespace MainMikitan.Application.Features.Customer.Commands;

public class CustomerDeleteAccountCommand(int customerUserId) : ICommand
{
    public int CustomerUserId { get; } = customerUserId;
}

public class CustomerDeleteAccountCommandHandler(
    IEmailSenderService emailSenderService,
    IOtpLogCommandRepository otpLogCommandRepository,
    IOptions<OtpOptions> otpOptions,
    ICustomerQueryRepository customerQueryRepository,
    IReservationCommandRepository reservationCommandRepository)
    : ResponseMaker, ICommandHandler<CustomerDeleteAccountCommand>
{
    public async Task<ResponseModel<bool>> Handle(CustomerDeleteAccountCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var hasAnyReservation =
                await reservationCommandRepository.HasAnyActiveReservationByCustomerId(request.CustomerUserId);
            if (hasAnyReservation)
                return Fail(ErrorType.Reservation.CustomerHasReservation);
            var customer = await customerQueryRepository.GetById(request.CustomerUserId);
            if (customer is null)
                return Fail(ErrorType.Customer.NotFound);
            var emailBuilder = new EmailSenderService.EmailBuilder();
            var otp = OtpGenerator.OtpGenerate();
            emailBuilder.AddReplacement("{OTP}", otp);
            var emailSenderResult =
                await emailSenderService.SendEmailAsync(customer.EmailAddress, emailBuilder, (int)Enums.EmailType.CustomerRegistrationEmail);
            if(!emailSenderResult) return Fail(ErrorType.EmailSender.EmailNotSend);
            var otpLogResult = await otpLogCommandRepository.Create(new Domain.Models.Common.OtpLogIntroEntity
            {
                EmailAddress = customer.EmailAddress,
                NumberOfTrialsIsRequired = false,
                Otp = otp,
                UserTypeId = (int)Enums.UserTypeId.CustomerIntro,
                ValidationTime = otpOptions.Value.IntroValidationTime,
                OperationId = (int)Enums.OtpOperationTypeId.CustomerDeleteAccount
            });
            return !otpLogResult 
                ? Fail(ErrorType.OtpLog.OtpLogNotCreated) 
                : Success();
        }
        catch (Exception ex)
        {
            return Unexpected(ex) ;
        }
    }
}