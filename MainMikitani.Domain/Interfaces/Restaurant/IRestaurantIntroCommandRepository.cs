using MainMikitan.Domain.Models.Customer;
using MainMikitan.Domain.Models.Restaurant;

namespace MainMikitan.Database.Features.Restaurant.Command {
    public interface IRestaurantIntroCommandRepository {
        Task<int> Create(RestaurantIntroEntity entity);
    }
}