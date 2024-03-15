using MainMikitan.Domain.Models.Restaurant;

namespace MainMikitan.Database.Features.Reservation.Dapper.Interface;

public interface IRestaurantDeActivationDapperQueryRepository
{
    Task<List<RestaurantDeActivationEntity>?> GetAll();
}