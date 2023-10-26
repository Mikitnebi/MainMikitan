using MainMikitan.Database.Features.Common.Multifunctional.Interface.Repository;
using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain.Models.Restaurant;
using MainMikitan.Domain.Models.Setting;
using Microsoft.Extensions.Options;

namespace MainMikitan.Database.Features.Restaurant.Command
{
    public class RestaurantCommandRepository : IRestaurantCommandRepository
    {
        private readonly ConnectionStringsOptions _connectionString;
        private readonly IMultifunctionalRepository _multifunctionalRepository;
        public RestaurantCommandRepository(IOptions<ConnectionStringsOptions> connectionString, IMultifunctionalRepository multifunctionalRepository)
        {
            _connectionString = connectionString.Value;
            _multifunctionalRepository = multifunctionalRepository;
        }

        public async Task<int> Create(RestaurantEntity restaurant)
        {
            restaurant.CreatedAt = DateTime.Now;
            return await _multifunctionalRepository.AddOrUpdateTableData<RestaurantEntity>(restaurant, "MainMikitan", "dbo", "Restaurant");
        }
    }
}
