using System.Data.SqlClient;
using Dapper;
using MainMikitan.Domain.Models.Setting;
using MainMikitan.Domain.Requests.RestaurantRequests;
using Microsoft.Extensions.Options;

namespace MainMikitan.Database.Features.Restaurant.Query;

public class FillRestaurantInfoIntroRepository
{
    private readonly ConnectionStringsOptions _connectionString;
    public FillRestaurantInfoIntroRepository(
        IOptions<ConnectionStringsOptions> connectionStrings
    ) 
    {
        _connectionString = connectionStrings.Value;
    }

    public async Task<int> FillIntroInfo(RestaurantRegistrationFinalRequest request)
    {
        var connection = new SqlConnection(_connectionString.MainMik);
        await connection.OpenAsync();

        var sqlCommand =
            $"INSERT INTO [MainMikitan].[dbo].[RestaurantInfo] (Address, ManagerId, BusinessTypeId, CoupeQuantity, TableQuantity, TerraceQuantity, HallStartHour, HallEndHour, KitchenStartHour, KitchenEndHour, MusicStartHour, MusicEndHour, Description, ActiveStatusId, ForCorporateEvents, LocationX, LocationY, HallStartMinute, HallEndMinute, KitchenStartMinute, KitchenEndMinute, MusicStartMinute, MusicEndMinute, UpdateUserId, UpdateAt, CreateAt) " +
            $"VALUES (@Address, @ManagerId, @BusinessTypeId, @CoupeQuantity, @TableQuantity, @TerraceQuantity, @HallStartHour, @HallEndHour, @KitchenStartHour, @KitchenEndHour, @MusicStartHour, @MusicEndHour, @Description, @ActiveStatusId, @ForCorporateEvents, @LocationX, @LocationY, @HallStartMinute, @HallEndMinute, @KitchenStartMinute, @KitchenEndMinute, @MusicStartMinute, @MusicEndMinute, @UpdateUserId, @UpdateAt, @CreateAt)";

        return await connection.ExecuteAsync(sqlCommand, request);
    }
}