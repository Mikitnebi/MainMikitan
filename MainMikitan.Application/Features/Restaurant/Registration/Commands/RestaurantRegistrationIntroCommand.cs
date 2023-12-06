using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.RestaurantRequests;
using MediatR;
using MainMikitan.Database.Features.Restaurant.Command;
using MainMikitan.Domain.Models.Restaurant;
using MainMikitan.Domain;
using static MainMikitan.ExternalServicesAdapter.Email.EmailSenderService;
using MainMikitan.ExternalServicesAdapter.Email;
using MainMikitan.Database.Features.Common.Otp.Interfaces;
using MainMikitan.Domain.Models.Setting;
using Microsoft.Extensions.Options;
using static MainMikitan.Domain.Enums;
using MainMikitan.InternalServiceAdapter.Validations;
using MainMikitan.InternalServicesAdapter.Validations;
using MainMikitan.InternalServiceAdapter.OtpGenerator;

namespace MainMikitan.Application.Features.Restaurant.Registration.Commands {
    public class RestaurantRegistrationIntroCommand : IRequest<ResponseModel<bool>> {
        public RestaurantRegistrationIntroRequest _restaurantRegistrationIntroRequest { get; set; } 
        
        public RestaurantRegistrationIntroCommand(RestaurantRegistrationIntroRequest request) {
            _restaurantRegistrationIntroRequest = request;
        }
    }
    public class RestaurantRegistrationIntroCommandHandler : IRequestHandler<RestaurantRegistrationIntroCommand, ResponseModel<bool>> {
        private readonly IRestaurantIntroCommandRepository _restaurantIntroCommandRepository;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IOtpLogCommandRepository _otpLogCommandRepository;
        private readonly OtpOptions _otpConfig;

        public RestaurantRegistrationIntroCommandHandler(
            IRestaurantIntroCommandRepository restaurantIntroCommandRepository,
            IEmailSenderService emailSenderService,
            IOtpLogCommandRepository otpLogCommandRepository,
            IOptions<OtpOptions> otpConfig) {
            _restaurantIntroCommandRepository = restaurantIntroCommandRepository;
            _emailSenderService = emailSenderService;
            _otpLogCommandRepository = otpLogCommandRepository;
            _otpConfig = otpConfig.Value;
        }

        public async Task<ResponseModel<bool>> Handle(RestaurantRegistrationIntroCommand command, CancellationToken cancellationToken) {
            var response = new ResponseModel<bool>();
            var registrationRequest = command._restaurantRegistrationIntroRequest;
            try {
                var validation = RestaurantRequestsValidation.RegistrationIntro(registrationRequest);
                if (validation.HasError) return validation;
                var email = registrationRequest.EmailAddress;
                var emailBuilder = new EmailBuilder();
                var otp = OtpGenerator.OtpGenerate();
                emailBuilder.AddReplacement("{OTP}", otp);
                var emailSenderResult = await _emailSenderService.SendEmailAsync(email, emailBuilder, (int)EmailType.RestaurantRegistrationEmail);
                var otpLogResult = await _otpLogCommandRepository.Create(new Domain.Models.Common.OtpLogIntroEntity {
                    UserTypeId = (int)UserTypeId.RestaurantIntro,
                    Otp = otp,
                    EmailAddress = email,
                    NumberOfTrialsIsRequired = false,
                    ValidationTime = _otpConfig.IntroValidationTime
                });
                var createRestaurantResult = await _restaurantIntroCommandRepository.Create(new RestaurantIntroEntity {
                    PhoneNumber = registrationRequest.PhoneNumber,
                    RegionId = registrationRequest.RegionId,
                    EmailAddress = registrationRequest.EmailAddress,
                    BusinessNameGeo = registrationRequest.BusinessNameGeo,
                    BusinessNameEng = registrationRequest.BusinessNameEng,
                    RestaurantOtpVerificationId = (int)RestaurantOtpVerificationId.NonVerified
                }); 
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

    
