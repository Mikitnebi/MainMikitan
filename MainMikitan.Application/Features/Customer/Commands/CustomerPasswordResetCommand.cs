using MainMikitan.Application.Services.CacheServices;
using MainMikitan.Database.Features.Common.Otp.Interfaces;
using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Models.Setting;
using MainMikitan.Domain.Templates;
using MainMikitan.ExternalServicesAdapter.Email;
using MainMikitan.InternalServiceAdapter.OtpGenerator;
using Microsoft.Extensions.Options;

namespace MainMikitan.Application.Features.Customer.Commands;

public class CustomerPasswordResetCommand(int customerId) : ICommand
{
    public readonly int CustomerId = customerId;
}

public class CustomerPasswordResetCommandHandler(
    ICustomerQueryRepository customerQueryRepository,
    IOtpLogCommandRepository otpLogCommandRepository,
    IEmailSenderService emailSenderService,
    IOptions<OtpOptions> otpOptions) : ResponseMaker, ICommandHandler<CustomerPasswordResetCommand>
{
    private readonly OtpOptions _otpOptions = otpOptions.Value;
    public async Task<ResponseModel<bool>> Handle(CustomerPasswordResetCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var customer = await customerQueryRepository.GetById(request.CustomerId);
            if (customer is null)
                return Fail(ErrorType.Customer.NotFound);
            var emailBuilder = new EmailSenderService.EmailBuilder();
            var otp = OtpGenerator.OtpGenerate();
            emailBuilder.AddReplacement("{OTP}", otp);
            var emailSenderResult =
                await emailSenderService.SendEmailAsync(customer.EmailAddress, emailBuilder, (int)Enums.EmailType.CustomerPasswordResetEmail);
            if(!emailSenderResult) return Fail(ErrorType.EmailSender.EmailNotSend);
            var otpLogResult = await otpLogCommandRepository.Create(new Domain.Models.Common.OtpLogIntroEntity
            {
                EmailAddress = customer.EmailAddress,
                NumberOfTrialsIsRequired = false,
                Otp = otp,
                UserTypeId = (int)Enums.UserTypeId.Customer,
                ValidationTime = otpOptions.Value.IntroValidationTime,
                OperationId = (int)Enums.OtpOperationTypeId.CustomerPasswordReset
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