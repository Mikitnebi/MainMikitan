using MainMikitan.Domain.Models.Restaurant;

namespace MainMikitan.Database.Features.Restaurant.Interface;

public interface IRestaurantBranchingCodeLogRepository
{
    Task<int> Create(RestaurantBranchingCodeLogEntity model);
}