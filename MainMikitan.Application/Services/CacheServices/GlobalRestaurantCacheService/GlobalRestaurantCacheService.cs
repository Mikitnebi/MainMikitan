using MainMikitan.Database.Features.Reservation.Interfaces;
using MainMikitan.Database.Features.Table.Interface;
using MainMikitan.Domain.Models.Restaurant.TableManagement;

namespace MainMikitan.Application.Services.CacheServices.GlobalRestaurantCacheService;

public class GlobalRestaurantCacheService
(
    ITableQueryRepository tableQueryRepository,
    IReservationQueryRepository reservationQueryRepository
    ) : IGlobalRestaurantCacheService
{
    public static Dictionary<DateOnly,
        Dictionary<int,
            Dictionary<int,
                List<SemTable>>>> RestaurantReservationSystem { get; set; } =
        new Dictionary<DateOnly, Dictionary<int, Dictionary<int, List<SemTable>>>>(21);
    
    public async Task<bool> Initilize()
    {
        var currentDateTime = DateTime.Now;
        var activeReservations = await reservationQueryRepository.GetActiveReservation();
        var allRestaurantAllTables = await tableQueryRepository.GetAllTable();
        for (var i = 0; i <= 14; i++)
        {
            currentDateTime = currentDateTime.AddDays(i);
            var currentDateTimeOnly = DateOnly.FromDateTime(currentDateTime);
            RestaurantReservationSystem.Add(currentDateTimeOnly, new Dictionary<int, Dictionary<int, List<SemTable>>>(20));
        }
        return true;
    }

    private void InitializeTables(List<TableInfoEntity> allRestaurantAllTables)
    {
        
    }
}





public class SemTable
{
    public int TableId { get; set; }
    public SemaphoreSlim TableLock= new SemaphoreSlim(1, 1);
}
