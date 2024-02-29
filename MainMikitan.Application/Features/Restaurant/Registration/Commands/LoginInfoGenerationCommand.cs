using MainMikitan.Database.Features.Common.Email.Interfaces;
using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain;
using MainMikitan.Domain.Interfaces.Restaurant;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Models.Restaurant;
using MainMikitan.Domain.Requests.RestaurantRequests;
using MainMikitan.Domain.Templates;
using MainMikitan.ExternalServicesAdapter.Email;
using MainMikitan.InternalServiceAdapter.Hasher;
using MainMikitan.InternalServicesAdapter.Util;

namespace MainMikitan.Application.Features.Restaurant.Registration.Commands
{
    public class LoginInfoGenerationCommand(RestaurantRegistrationRequest restaurant, RestaurantStaffRegistrationRequest staff) : ICommand
    {
        public readonly RestaurantRegistrationRequest Restaurant = restaurant;
        public readonly RestaurantStaffRegistrationRequest Staff = staff;
    }
    public class LoginInfoGenerationCommandHandler(
        IRestaurantIntroQueryRepository restaurantIntroQueryRepository,
        IRestaurantCommandRepository restaurantCommandRepository,
        IEmailSenderQueryRepository emailSenderQueryRepository,
        IRestaurantStaffCommandRepository restaurantStaffCommandRepository,
        IPasswordHasher passwordHasher,
        IEmailSenderService emailSenderService)
        : ResponseMaker, ICommandHandler<LoginInfoGenerationCommand>
    {
        public async Task<ResponseModel<bool>> Handle(LoginInfoGenerationCommand request, CancellationToken cancellationToken)
        {
            var getRestaurantIntro = await restaurantIntroQueryRepository.GetVerifiedByEmail(request.Restaurant.Email, cancellationToken);
            if(getRestaurantIntro == null) 
                return Fail(ErrorType.RestaurantIntro.VerifiedRestaurantNotFound);
            
            var generateUserName = UtilHelper.GenerateUserName();
            var generatePassword = UtilHelper.GeneratePassword();

            var restaurant = new RestaurantEntity
            {
                OfficialEmail = request.Restaurant.OfficialEmail,
                BusinessNameGeo = request.Restaurant.BusinessNameGeo,
                BusinessNameEng = request.Restaurant.BusinessNameEng,
                StatusId = (int) Enums.RestaurantActiveStatus.TemporaryClosed,
                CreatedAt = DateTime.Now
            };
            
            var addRestaurant = await restaurantCommandRepository.Create(restaurant, cancellationToken);
            var saveRestaurant = await restaurantCommandRepository.SaveChanges(cancellationToken);
            if(!addRestaurant || !saveRestaurant)
                return Fail(ErrorType.Restaurant.NotUpdated);

            var manager = new RestaurantStaffEntity
            {
                Email = request.Staff.Email!,
                FullNameGeo = request.Staff.FullNameGeo,
                FullNameEng = request.Staff.FullNameEng,
                PhoneNumber = request.Staff.PhoneNumber,
                CreatedAt = DateTime.Now,
                IsManager = true,
                IsActive = true,
                PasswordHash = passwordHasher.Hash(generatePassword),
                UserNameHash = passwordHasher.Hash(generateUserName),
                RestaurantId = restaurant.Id,
                EmailConfirmation = false
            };

            var addStaff = await restaurantStaffCommandRepository.Add(manager, cancellationToken);
            var saveStaff = await restaurantStaffCommandRepository.SaveChanges(cancellationToken);
            if(!addStaff || !saveStaff)
                return Fail(ErrorType.Staff.NotAdded);
            
            var sendEmail = await emailSenderQueryRepository.GetEmailById((int)Enums.EmailType.ManagerGenerateAccount, cancellationToken);
            if (sendEmail == null)
                return Fail(ErrorType.Restaurant.EmailTypeNotFound);
            
            var emailBuilder = new EmailSenderService.EmailBuilder();
            emailBuilder.AddReplacement("{Username}", generateUserName);
            emailBuilder.AddReplacement("{Password}", generatePassword);
            
            var emailSenderResult = await emailSenderService.SendEmailAsync(
                request.Staff.Email!, emailBuilder, 
                (int)Enums.EmailType.ManagerGenerateAccount);

            return !emailSenderResult ? Fail(ErrorType.EmailSender.EmailNotSend) : Success();
        }
    }
}
