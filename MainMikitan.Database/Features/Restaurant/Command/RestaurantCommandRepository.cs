using MainMikitan.Database.Features.Common.Multifunctional.Interface.Repository;
using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain.Models.Restaurant;
using MainMikitan.Domain.Models.Setting;
using Microsoft.Extensions.Options;

namespace MainMikitan.Database.Features.Restaurant.Command
{
    public class RestaurantCommandRepository(
        IOptions<ConnectionStringsOptions> connectionString,
        IMultifunctionalRepository multifunctionalRepository)
        : IRestaurantCommandRepository
    {
        private readonly ConnectionStringsOptions _connectionString = connectionString.Value;

        public async Task<int> Create(RestaurantEntity restaurant)
        {
            restaurant.CreatedAt = DateTime.Now;
            return await multifunctionalRepository.AddOrUpdateTableData<RestaurantEntity>(restaurant, "MainMikitan", "dbo", "Restaurant");
        }
    }
}
