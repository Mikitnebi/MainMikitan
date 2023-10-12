using MainMikitan.Domain.Models.Restaurant;

namespace MainMikitan.Database.Features.Restaurant.Interface
{
    public interface IRestaurantFinalCommandRepository
    {
        public Task<int> Create(RestaurantStarterInfo entity);
    }
}