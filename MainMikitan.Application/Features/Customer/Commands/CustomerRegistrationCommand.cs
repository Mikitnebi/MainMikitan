using Azure.Core;
using MainMikitan.InternalServiceAdapter.OtpGenerator;
using MainMikitan.InternalServiceAdapter.Validations;
using MainMikitan.Database.Features.Common.Otp.Interfaces;
using MainMikitan.Domain;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Models.Customer;
using MainMikitan.Domain.Models.Setting;
using MainMikitan.Domain.Requests;
using MainMikitan.ExternalServicesAdapter.Email;
using Microsoft.Extensions.Options;
using static MainMikitan.Domain.Enums;
using static MainMikitan.ExternalServicesAdapter.Email.EmailSenderService;
using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain.Templates;
using MainMikitan.InternalServiceAdapter.Hasher;

namespace MainMikitan.Application.Features.Customer.Commands {
    public class CustomerRegistrationCommand(CustomerRegistrationRequest request) : ICommand
    {
        public CustomerRegistrationRequest RegistrationRequest { get; set; } = request;
    }
    public class CustomerRegistrationCommandHandler(
        ICustomerQueryRepository customerQueryRepository,
        ICustomerCommandRepository customerCommandRepository,
        IPasswordHasher passwordHasher,
        IEmailSenderService emailSenderService,
        IOptions<OtpOptions> otpConfig,
        IOtpLogCommandRepository otpLogCommandRepository)
        : ResponseMaker, ICommandHandler<CustomerRegistrationCommand>
    {
        private readonly OtpOptions _otpConfig = otpConfig.Value;


        public async Task<ResponseModel<bool>> Handle(CustomerRegistrationCommand command, CancellationToken cancellationToken) {
            var registrationRequest = command.RegistrationRequest;
            try {
                var email = registrationRequest.Email!.ToUpper();
                var mobile = registrationRequest.MobileNumber;
                var validation = CustomerRequestsValidation.Registration(registrationRequest);
                if (validation.HasError) return validation;

                if (command.RegistrationRequest.RequiredOptions)
                {
                    var emailValidation = await customerQueryRepository.GetByEmail(email);
                    if (emailValidation != null)
                        return Fail(ErrorType.AlreadyUsedEmail);
                    //var mobileNumberValidation = await customerQueryRepository.GetByMobileNumber(registrationRequest.MobileNumber!);
                    var emailBuilder = new EmailBuilder();
                    var otp = OtpGenerator.OtpGenerate();
                    emailBuilder.AddReplacement("{OTP}", otp);
                    var emailSenderResult = await emailSenderService.SendEmailAsync(email, emailBuilder, (int)EmailType.CustomerRegistrationEmail);
                    if (!emailSenderResult)
                        return Fail(ErrorType.EmailSender.EmailNotSend);
                    var otpLogResult = await otpLogCommandRepository.Create(new Domain.Models.Common.OtpLogIntroEntity
                    {
                        EmailAddress = email,
                        NumberOfTrialsIsRequired = false,
                        Otp = otp,
                        UserTypeId = (int)UserTypeId.CustomerIntro,
                        ValidationTime = _otpConfig.IntroValidationTime
                    });
                    if (!otpLogResult)
                        return Fail(ErrorType.OtpLog.OtpLogNotCreated);
                    var customerEntity = new CustomerEntity
                    {
                        EmailAddress = email,
                        FullName = $"{registrationRequest.FirstName} {registrationRequest.LastName}",
                        MobileNumber = registrationRequest.MobileNumber!,
                        HashPassWord = registrationRequest.Password
                    };
                    var createCustomerResult = await customerCommandRepository.CreateOrUpdate(customerEntity);
                    if (!createCustomerResult)
                        return Fail(ErrorType.Customer.NotUpdated);
                }
                return Success();
            } catch (Exception ex) {
                return Unexpected(ex);
            }
        }
    }
}
