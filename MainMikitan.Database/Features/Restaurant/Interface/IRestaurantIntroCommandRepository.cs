using MainMikitan.Domain.Models.Customer;
using MainMikitan.Domain.Models.Restaurant;
using static MainMikitan.Domain.Enums;

namespace MainMikitan.Database.Features.Restaurant.Command {
    public interface IRestaurantIntroCommandRepository {
        Task<int?> UpdateStatus(string email, bool emailConfirmation, RestaurantOtpVerificationId status);
        Task<int> Create(RestaurantIntroEntity entity);
    }
}