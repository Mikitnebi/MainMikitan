using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain.Models.Restaurant;
using MainMikitan.Domain.Models.Setting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MainMikitan.Domain.Enums;

namespace MainMikitan.Database.Features.Restaurant.Command {
    public class RestaurantFinalCommandRepository : IRestaurantFinalCommandRepository {
        private readonly ConnectionStringsOptions _connectionStrings;

        public Task<int> Create(RestaurantStarterInfo entity) {
            /*using var connection = new SqlConnection(_connectionStrings.MainMik);
            entity.UpdateDate = DateTime.Now;
            var sqlCommand = "INSERT INTO [dbo].[RestaurantInfo]" +
                "([],)"
*/
            return null;
        }
    }
}
