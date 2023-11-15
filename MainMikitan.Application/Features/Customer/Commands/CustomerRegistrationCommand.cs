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

namespace MainMikitan.Application.Features.Customer.Commands {
    public class CustomerRegistrationCommand : ICommand {
        public CustomerRegistrationRequest RegistrationRequest { get; set; }
        public CustomerRegistrationCommand(CustomerRegistrationRequest request) {
            RegistrationRequest = request;
        }
    }
    public class CustomorRegistrationCommandHandler : ICommandHandler<CustomerRegistrationCommand> {
        private readonly ICustomerQueryRepository _customerQueryRepository;
        private readonly ICustomerCommandRepository _customerCommandRepository;
        private readonly IOtpLogCommandRepository _otpLogCommandRepository;
        private readonly IEmailSenderService _emailSenderService;
        private readonly OtpOptions _otpConfig; 
        public CustomorRegistrationCommandHandler(
            ICustomerQueryRepository customerQueryRepository,
            ICustomerCommandRepository customerCommandRepository,
            IEmailSenderService emailSenderService,
            IOptions<OtpOptions> otpConfig,
            IOtpLogCommandRepository otpLogCommandRepository)
        {
            _otpConfig = otpConfig.Value;
            _emailSenderService = emailSenderService;
            _customerCommandRepository = customerCommandRepository;
            _customerQueryRepository = customerQueryRepository;
            _otpLogCommandRepository = otpLogCommandRepository;
        }


        public async Task<ResponseModel<bool>> Handle(CustomerRegistrationCommand command, CancellationToken cancellationToken) {
            var response = new ResponseModel<bool>();
            var registrationRequest = command.RegistrationRequest;
            try {
                var email = registrationRequest.Email!.ToUpper();
                var mobilde = registrationRequest.MobileNumber;
                var validation = CustomerRequestsValidation.Registration(registrationRequest);
                if (validation.HasError) return validation;

                if (command.RegistrationRequest.RequiredOptions)
                {
                    var emailValidation = await _customerQueryRepository.GetByEmail(email);
                    if (emailValidation != null)
                    {
                        response.ErrorType = ErrorType.AlreadyUsedEmail;
                        return response;
                    }
                    var mobileNumberValidation = await _customerQueryRepository.GetByMobileNumber(registrationRequest.MobileNumber!);

                    var emailBuilder = new EmailBuilder();
                    var otp = OtpGenerator.OtpGenerate();
                    emailBuilder.AddReplacement("{OTP}", otp);
                    var emailSenderResult = await _emailSenderService.SendEmailAsync(email, emailBuilder, (int)EmailType.CustomerRegistrationEmail);

                    var otpLogResult = await _otpLogCommandRepository.Create(new Domain.Models.Common.OtpLogIntroEntity
                    {
                        EmailAddress = email,
                        NumberOfTrialsIsRequired = false,
                        Otp = otp,
                        UserTypeId = (int)UserTypeId.CustomerIntro,
                        ValidationTime = _otpConfig.IntroValidationTime
                    });

                    var createCustomerResult = await _customerCommandRepository.CreateOrUpdate(new CustomerEntity
                    {
                        EmailAddress = email,
                        FullName = $"{registrationRequest.FirstName} {registrationRequest.LastName}",
                        MobileNumber = registrationRequest.MobileNumber!,
                        HashPassWord = registrationRequest.Password
                    });
                }
                response.Result = true;
                return response;
            } catch (Exception ex) {
                response.ErrorType = ErrorType.UnExpectedException;
                response.ErrorMessage = ex.Message;
                return response;
            }
        }
    }
}
