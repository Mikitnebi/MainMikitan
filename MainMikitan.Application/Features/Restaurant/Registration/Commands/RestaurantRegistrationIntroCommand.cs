using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.RestaurantRequests;
using MediatR;
using MainMikitan.Database.Features.Restaurant.Command;
using MainMikitan.Domain.Models.Restaurant;
using MainMikitan.Domain;
using static MainMikitan.ExternalServicesAdapter.Email.EmailSenderService;
using MainMikitan.ExternalServicesAdapter.Email;
using MainMikitan.Database.Features.Common.Otp.Interfaces;
using MainMikitan.Domain.Interfaces.Restaurant;
using MainMikitan.Domain.Models.Setting;
using MainMikitan.Domain.Templates;
using Microsoft.Extensions.Options;
using static MainMikitan.Domain.Enums;
using MainMikitan.InternalServiceAdapter.Validations;
using MainMikitan.InternalServicesAdapter.Validations;
using MainMikitan.InternalServiceAdapter.OtpGenerator;

namespace MainMikitan.Application.Features.Restaurant.Registration.Commands {
    public class RestaurantRegistrationIntroCommand(RestaurantRegistrationIntroRequest request)
        : ICommand
    {
        public RestaurantRegistrationIntroRequest _restaurantRegistrationIntroRequest { get; set; } = request;
    }
    public class RestaurantRegistrationIntroCommandHandler(
        IRestaurantIntroCommandRepository restaurantIntroCommandRepository,
        IRestaurantIntroQueryRepository restaurantIntroQueryRepository,
        IEmailSenderService emailSenderService,
        IOtpLogCommandRepository otpLogCommandRepository,
        IOptions<OtpOptions> otpConfig)
        : ResponseMaker, ICommandHandler<RestaurantRegistrationIntroCommand>
    {
        private readonly OtpOptions _otpConfig = otpConfig.Value;

        public async Task<ResponseModel<bool>> Handle(RestaurantRegistrationIntroCommand command, CancellationToken cancellationToken) 
        {
            var registrationRequest = command._restaurantRegistrationIntroRequest;
            try {
                var validation = RestaurantRequestsValidation.RegistrationIntro(registrationRequest);
                if (validation.HasError) return validation;
                var email = registrationRequest.EmailAddress;
                var existedRestaurant =
                    await restaurantIntroQueryRepository.GetVerifiedByEmail(email, cancellationToken);
                if (existedRestaurant is not null)
                    return Fail(ErrorType.RestaurantIntro.RestaurantWithThisMailAlreadyExisted);
                var emailBuilder = new EmailBuilder();
                var otp = OtpGenerator.OtpGenerate();
                emailBuilder.AddReplacement("{OTP}", otp);
                var emailSenderResult = await emailSenderService.SendEmailAsync(email, emailBuilder, (int)EmailType.RestaurantRegistrationEmail);
                var otpLogResult = await otpLogCommandRepository.Create(new Domain.Models.Common.OtpLogIntroEntity {
                    UserTypeId = (int)UserTypeId.RestaurantIntro,
                    Otp = otp,
                    EmailAddress = email,
                    NumberOfTrialsIsRequired = false,
                    ValidationTime = _otpConfig.IntroValidationTime  ,
                    OperationId = (int)Enums.OtpOperationTypeId.RestaurantIntroRegistration
                }, cancellationToken);
                var createRestaurantResult = await restaurantIntroCommandRepository.Create(new RestaurantIntroEntity {
                    PhoneNumber = registrationRequest.PhoneNumber,
                    RegionId = registrationRequest.RegionId,
                    EmailAddress = registrationRequest.EmailAddress,
                    BusinessNameGeo = registrationRequest.BusinessNameGeo,
                    BusinessNameEng = registrationRequest.BusinessNameEng,
                    CreatedAt = DateTime.Now,
                    StatusId = (int)RestaurantStatusId.NonVerified,
                    EmailConfirmation = false
                }, cancellationToken);
                var saveResponse = await restaurantIntroCommandRepository.SaveChanges(cancellationToken);
                return Success();
            } catch (Exception ex)
            {
                return Unexpected(ex);
            }
        }
    }
}

    
