using MainMikitan.Database.Features.Common.Email.Interfaces;
using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Interfaces.Restaurant;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Models.Restaurant;
using MainMikitan.Domain.Requests.RestaurantRequests;
using MainMikitan.ExternalServicesAdapter.Email;
using MainMikitan.InternalServiceAdapter.Hasher;
using MainMikitan.InternalServicesAdapter.Util;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MainMikitan.Application.Features.Restaurant.Registration.Commands
{
    public class LoginInfoGeneratironCommand : IRequest<ResponseModel<bool>>
    {
        public string _email { get; set; }
        public bool _sendAsEmail { get; set; }
        public LoginInfoGeneratironCommand(LoginInfoGeneratironRequest request)
        {
            _email = request.Email;
            _sendAsEmail= request.SendAsEmail;
        }
    }
    public class LoginInfoGeneratironCommandHandler : IRequestHandler<LoginInfoGeneratironCommand, ResponseModel<bool>>
    {
        private readonly IRestaurantIntroQueryRepository _restaurantIntroQueryRepository;
        private readonly IRestaurantCommandRepository _restaurantCommandRepository;
        private readonly IEmailSenderQueryRepository _emailSenderQueryRepository;
        private readonly IEmailSenderService _emailSenderService;
        public LoginInfoGeneratironCommandHandler(
            IRestaurantIntroQueryRepository restaurantIntroQueryRepository, 
            IRestaurantCommandRepository restaurantCommandRepository, 
            IEmailSenderQueryRepository emailSenderQueryRepository, 
            IEmailSenderService emailSenderService)
        {
            _restaurantIntroQueryRepository = restaurantIntroQueryRepository;
            _restaurantCommandRepository = restaurantCommandRepository;
            _emailSenderQueryRepository = emailSenderQueryRepository;
            _emailSenderService = emailSenderService;
        }
        public async Task<ResponseModel<bool>> Handle(LoginInfoGeneratironCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseModel<bool>();
            var email = request._email;
            var sendAsEmail = request._sendAsEmail;
            var getRestaurantIntro = await _restaurantIntroQueryRepository.GetVerifiedByEmail(email);
            if(getRestaurantIntro == null)
            {
                response.ErrorType = ErrorType.RestaurantIntro.VerifiedRestaurantNotFound;
                return response;
            }
            var generateUserName = UtilHelper.GenerateUserName();
            var generatePassword = UtilHelper.GeneratePassword();

            var hasher = new PasswordHasher<RestaurantEntity>();
            
            var restaurant = new RestaurantEntity
            {
                UserName = generateUserName
            };

            restaurant.PasswordHash = hasher.HashPassword(restaurant, generatePassword);
            
            var addRestaurant = await _restaurantCommandRepository.Create(restaurant);
            if(addRestaurant == 0)
            {
                response.ErrorType = ErrorType.Restaurant.RestaurantNotUpdated;
                return response;
            }
            var sendEmail = await _emailSenderQueryRepository.GetEmailById((int)Enums.EmailType.RestaurantGenerateAccount);

            var emailBuilder = new EmailSenderService.EmailBuilder();
            
            var emailSenderResult = await _emailSenderService.SendEmailAsync(email, emailBuilder, (int)Enums.EmailType.RestaurantGenerateAccount);

            if(!emailSenderResult)
            {
                response.ErrorMessage = ErrorType.EmailSender.EmailNotSend;
            }
            
            response.Result = true;
            return response;
        }
    }
}
