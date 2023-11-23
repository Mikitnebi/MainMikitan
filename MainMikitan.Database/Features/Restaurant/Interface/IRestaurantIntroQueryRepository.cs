using MainMikitan.Domain.Models.Restaurant;

namespace MainMikitan.Domain.Interfaces.Restaurant;

public interface IRestaurantIntroQueryRepository {
    Task<RestaurantIntroEntity> GetNonVerifiedByEmail(string email);
    Task<RestaurantIntroEntity> GetVerifiedByEmail(string email);
    Task<RestaurantEntity> GetByUsername(string username);
}
