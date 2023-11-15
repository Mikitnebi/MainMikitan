using MainMikitan.Database.Features.Common.Otp.Interfaces;
using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Models.Customer;
using MainMikitan.Domain.Models.Setting;
using MainMikitan.Domain.Templates;
using MainMikitan.ExternalServicesAdapter.Email;
using MainMikitan.ExternalServicesAdapter.S3ServiceAdapter;
using MainMikitan.InternalServiceAdapter.OtpGenerator;
using Microsoft.Extensions.Options;

namespace MainMikitan.Application.Features.Customer.Commands;

public class CustomerDeleteAccountCommand : ICommand
{
    public int CustomerUserId { get; }
    public string CustomerEmail { get; }
    public CustomerDeleteAccountCommand(int customerUserId, string customerEmail)
    {
        CustomerUserId = customerUserId;
        CustomerEmail = customerEmail;
    }
}

public class CustomerDeleteAccountCommandHandler : ICommandHandler<CustomerDeleteAccountCommand>
{
    private readonly IEmailSenderService _emailSenderService;
    private readonly IOtpLogCommandRepository _otpLogCommandRepository;
    private readonly OtpOptions _otpConfig;
    public CustomerDeleteAccountCommandHandler(
        IEmailSenderService emailSenderService, 
        IOtpLogCommandRepository otpLogCommandRepository, 
        IOptions<OtpOptions> otpOptions
        )
    {
        _otpLogCommandRepository = otpLogCommandRepository;
        _otpConfig = otpOptions.Value;
        _emailSenderService = emailSenderService;
    }

    public async Task<ResponseModel<bool>> Handle(CustomerDeleteAccountCommand request, CancellationToken cancellationToken)
    { 
        var response = new ResponseModel<bool>();
        try
        {
            // TODO: შემოწმდეს თუ ამ ტიპს აქვს რაიმე მიმდინარე შეკვეთა
            var emailBuilder = new EmailSenderService.EmailBuilder();
            var otp = OtpGenerator.OtpGenerate();
            emailBuilder.AddReplacement("{OTP}", otp);
            var emailSenderResult = await _emailSenderService.SendEmailAsync(request.CustomerEmail, emailBuilder, (int)Enums.EmailType.CustomerRegistrationEmail);

            var otpLogResult = await _otpLogCommandRepository.Create(new Domain.Models.Common.OtpLogIntroEntity
            {
                EmailAddress = request.CustomerEmail,
                NumberOfTrialsIsRequired = false,
                Otp = otp,
                UserTypeId = (int)Enums.UserTypeId.CustomerIntro,
                ValidationTime = _otpConfig.IntroValidationTime
            });
            response.Result = true;
            return response;
        }
        catch (Exception ex) {
            response.ErrorType = ErrorType.UnExpectedException;
            response.ErrorMessage = ex.Message;
            return response;
        }
    }
    
}