using Dapper;
using MainMikitan.Domain.Interfaces.Restaurant;
using MainMikitan.Domain.Models.Restaurant;
using MainMikitan.Domain.Models.Setting;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using static MainMikitan.Domain.Enums;

namespace MainMikitan.Database.Features.Restaurant.Command {
    public class RestaurantIntroCommandRepository : IRestaurantIntroCommandRepository {
        private readonly ConnectionStringsOptions _connectionStrings;
        private readonly IRestaurantIntroQueryRepository _restaurantIntroQueryRepository;

        public RestaurantIntroCommandRepository(
            IOptions<ConnectionStringsOptions> connectionStrings,
            IRestaurantIntroQueryRepository restaurantIntroQueryRepository) {
            _connectionStrings = connectionStrings.Value;
            _restaurantIntroQueryRepository = restaurantIntroQueryRepository;
        }
        public async Task<int?> UpdateStatus(string email, bool emailConfirmation, RestaurantOtpVerificationId status) {
            int statusId = (int)status;
            int confirmation = emailConfirmation == true ? 1 : 0;
            using var connection = new SqlConnection(_connectionStrings.MainMik);
            var restaurant = await _restaurantIntroQueryRepository.GetNonVerifiedByEmail(email);
            if (restaurant != null) {
                var sqlCommand = "UPDATE [dbo].[RestaurantIntro] " +
                    "SET [RestaurantOtpVerificationId] = @statusId, " +
                    "[EmailConfirmation] = @confirmation " +
                    "WHERE [Id] = @id";
                var result = await connection.ExecuteAsync(sqlCommand, new { statusId, confirmation, id = restaurant.Id });
                return result;
            }
            return restaurant == null ? 0 : restaurant.Id;
        }

        public async Task<int> Create(RestaurantIntroEntity entity) {

            using var connection = new SqlConnection(_connectionStrings.MainMik);
            entity.StatusId = (int)RestaurantStatusId.NonVerified;
            entity.CreatedAt = DateTime.Now;
            entity.RestaurantOtpVerificationId = 0;
            var sqlCommand = "INSERT INTO [dbo].[RestaurantIntro]" +
                "([StatusId]," +
                "[EmailAddress]," +
                "[EmailConfirmation]," +
                "[PhoneNumber]," +
                "[BusinessNameGeo]," +
                "[BusinessNameEng]," +
                "[RegionId]," +
                "[RestaurantOtpVerificationId]," + 
                "[CreatedAt])" +
                " OUTPUT INSERTED.Id" +
                " VALUES (@StatusId,@EmailAddress,@EmailConfirmation," +
                "@PhoneNumber, @BusinessNameGeo, @BusinessNameEng, @RegionId, @RestaurantOtpVerificationId, @CreatedAt)";
            var result = await connection.QueryFirstOrDefaultAsync<int>(sqlCommand, entity);
            return result;
        }
    }
}
