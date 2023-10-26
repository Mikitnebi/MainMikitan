using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain.Models.Restaurant;
using MainMikitan.Domain.Models.Setting;

namespace MainMikitan.Database.Features.Restaurant.Command {
    public class RestaurantFinalCommandRepository : IRestaurantFinalCommandRepository {
        private readonly ConnectionStringsOptions _connectionStrings;

        public Task<int> Create(RestaurantInfoEntity entity) {
            /*using var connection = new SqlConnection(_connectionStrings.MainMik);
            entity.UpdateDate = DateTime.Now;
            var sqlCommand = "INSERT INTO [dbo].[RestaurantInfo]" +
                "([],)"
*/
            return null;
        }
    }
}
