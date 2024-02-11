using MainMikitan.Domain.Models.Restaurant;

namespace MainMikitan.Domain.Interfaces.Restaurant;

public interface IRestaurantIntroQueryRepository {
    Task<RestaurantIntroEntity?> GetNonVerifiedByEmail(string email, CancellationToken cancellationToken = default);
    Task<RestaurantIntroEntity?> GetVerifiedByEmail(string email, CancellationToken cancellationToken = default);
}
