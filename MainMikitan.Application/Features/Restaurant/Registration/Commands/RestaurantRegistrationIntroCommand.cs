using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.RestaurantRequests;
using MainMikitan.Application.Features.Customer.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using MainMikitan.Common.Validations;
using FluentEmail.Core;
using MainMikitan.Database.Features.Restaurant.Command;
using MainMikitan.Domain.Models.Customer;
using System.Threading.Tasks.Sources;
using MainMikitan.Domain.Models.Restaurant;
using MainMikitan.Domain;
using MainMikitan.Common.OtpGenerator;
using static MainMikitan.ExternalServicesAdapter.Email.EmailSenderService;
using MainMikitan.ExternalServicesAdapter.Email;
using MainMikitan.Database.Features.Common.Otp.Interfaces;
using MainMikitan.Domain.Models.Setting;
using Microsoft.Extensions.Options;
using static MainMikitan.Domain.Enums;

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
            var registrtationRequest = command._restaurantRegistrationIntroRequest;
            try {
                var validation = RestaurantIntroRequestsValidation.Registration(registrtationRequest);
                if (validation.HasError) return validation;
                var email = registrtationRequest.EmailAddress;
                var emailBuilder = new EmailBuilder();
                var otp = OtpGenerator.OtpGenerate();
                emailBuilder.AddReplacement("{OTP}", otp);
                var emailSenderResult = await _emailSenderService.SendEmailAsync(email, emailBuilder, EmailType.RestaurantRegistrationEmail);
                var otpLogResult = await _otpLogCommandRepository.Create(new Domain.Models.Common.OtpLogIntroEntity {
                    UserTypeId = (int)UserTypeId.RestaurantIntro,
                    Otp = otp,
                    EmailAddress = email,
                    NumberOfTrialsIsRequired = false,
                    ValidationTime = _otpConfig.IntroValidationTime
                });
                var createRestaurantResult = await _restaurantIntroCommandRepository.Create(new RestaurantIntroEntity {
                    PhoneNumber = registrtationRequest.PhoneNumber,
                    RegionId = registrtationRequest.RegionId,
                    EmailAddress = registrtationRequest.EmailAddress,
                    BusinessName = registrtationRequest.BusinessName,
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

    
