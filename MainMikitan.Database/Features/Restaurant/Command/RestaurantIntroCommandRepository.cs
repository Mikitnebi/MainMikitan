using Dapper;
using MainMikitan.Domain.Models.Restaurant;
using MainMikitan.Domain.Models.Setting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MainMikitan.Domain.Enums;

namespace MainMikitan.Database.Features.Restaurant.Command {
    public class RestaurantIntroCommandRepository : IRestaurantIntroCommandRepository {
        private readonly ConnectionStringsOptions _connectionStrings;

        public RestaurantIntroCommandRepository(IOptions<ConnectionStringsOptions> connectionStrings) {
            _connectionStrings = connectionStrings.Value;
        }
        public async Task<int> Create(RestaurantIntroEntity entity) {
            using var connection = new SqlConnection(_connectionStrings.MainMik);
            entity.StatusId = (int)RestaurantStatusId.NonVerified;
            entity.JoinedAt = DateTime.Now;
            var sqlCommand = "INSERT INTO [basename].tablename" +
                "([StatusId]," +
                "[EmailAdress]," +
                "[EmailConfirmation]," +
                "[PhoneNumber]," +
                "[BusinessName]," +
                "[RegionId]," +
                "[CreatedAt]," +
                " OUTPUT INSERTED.Id" +
                " VALUES (@StatusId,@EmailAdress,@EmailConfirmation," +
                "@PhoneNumber, @BusinessName, @RegionId, @JoinedAt)";
            var result = await connection.QueryFirstOrDefaultAsync<int>(sqlCommand, entity);
            return result;
        }
    }
}
