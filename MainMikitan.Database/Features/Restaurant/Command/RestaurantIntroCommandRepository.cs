using Dapper;
using MainMikitan.Domain.Interfaces.Restaurant;
using MainMikitan.Domain.Models.Restaurant;
using MainMikitan.Domain.Models.Setting;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using MainMikitan.Database.DbContext;
using Microsoft.EntityFrameworkCore;
using static MainMikitan.Domain.Enums;

namespace MainMikitan.Database.Features.Restaurant.Command {
    public class RestaurantIntroCommandRepository(
        MikDbContext mikDbContext,
        IOptions<ConnectionStringsOptions> connectionStrings,
        IRestaurantIntroQueryRepository restaurantIntroQueryRepository)
        : IRestaurantIntroCommandRepository
    {
        private readonly ConnectionStringsOptions _connectionStrings = connectionStrings.Value;

        public async Task<int?> UpdateStatus(string email, bool emailConfirmation, RestaurantOtpVerificationId status) {
            int statusId = (int)status;
            int confirmation = emailConfirmation == true ? 1 : 0;
            using var connection = new SqlConnection(_connectionStrings.MainMik);
            var restaurant = await restaurantIntroQueryRepository.GetNonVerifiedByEmail(email);
            if (restaurant != null)
            {
                var sqlCommand = $"""
                                                      UPDATE [dbo].[RestaurantIntro] 
                                                      SET [StatusId] = @statusId, 
                                                      [EmailConfirmation] = @confirmation 
                                                      WHERE [Id] = @id
                                  """;
                var result = await connection.ExecuteAsync(sqlCommand, new { statusId, confirmation, id = restaurant.Id });
                return result;
            }
            return restaurant?.Id ?? 0;
        }

        public async Task<bool> Create(RestaurantIntroEntity entity, CancellationToken cancellationToken = default)
        {
            var restaurantIntroAddResponse = await mikDbContext.RestaurantIntro.AddAsync(entity, cancellationToken);
            return restaurantIntroAddResponse.State == EntityState.Added;
        }

        public async Task<bool> SaveChanges(CancellationToken cancellationToken = default)
        {
            var response = await mikDbContext.SaveChangesAsync(cancellationToken);
            return response > 0;
        }
    }
}
