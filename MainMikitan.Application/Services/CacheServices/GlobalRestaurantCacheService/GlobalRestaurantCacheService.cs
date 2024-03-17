using Amazon.Runtime.Internal;
using MainMikitan.Database.Features.Reservation.Dapper.Interface;
using MainMikitan.Database.Features.Reservation.Interfaces;
using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Database.Features.Table.Interface;
using MainMikitan.Domain.Models.Reservation;
using MainMikitan.Domain.Models.Restaurant;
using MainMikitan.Domain.Models.Restaurant.TableManagement;

namespace MainMikitan.Application.Services.CacheServices.GlobalRestaurantCacheService;

public class GlobalRestaurantCacheService
(
    ITableDapperQueryRepository tableDapperQueryRepository,
    ITableDapperEnvironmentQueryRepository tableDapperEnvironmentQuery,
    IReservationDapperQueryRepository reservationDapperQueryRepository,
    IRestaurantDeActivationDapperQueryRepository restaurantDeActivationDapperQueryRepository
    ) : IGlobalRestaurantCacheService
{
    public static readonly Dictionary<int, TableModel> TableModels = new Dictionary<int, TableModel>(20);

    public static readonly Dictionary<int, List<Tuple<DateTime, DateTime>>> RestaurantDeActivationTime =
        new Dictionary<int, List<Tuple<DateTime, DateTime>>>(20);

    public static Dictionary<DateOnly,
        Dictionary<int, Dictionary<int, List<TableLockModel>>>> RestaurantReservationSystem { get; set; } =
        new Dictionary<DateOnly, Dictionary<int, Dictionary<int, List<TableLockModel>>>>(20);
    
    public async Task<bool> Initilize()
    {
        await InitializeTables();
        await InitializeDeActiveRestaurant();
        var currentDateTime = DateTime.Now;
        var activeReservations = await reservationDapperQueryRepository.GetActiveReservation();
        for (var i = 0; i <= 7; i++)
        {
            currentDateTime = currentDateTime.AddDays(i);
            var currentDateTimeOnly = DateOnly.FromDateTime(currentDateTime);
            RestaurantReservationSystem.Add(currentDateTimeOnly, new Dictionary<int, Dictionary<int, List<TableLockModel>>>(20));
        }

        for (int i = 0; i < activeReservations.Count; i++)
        {
            var reservation = activeReservations[i];
            var reservationAt = DateOnly.FromDateTime(reservation.CreatedAt);
            if (!RestaurantReservationSystem.ContainsKey(reservationAt))
            {
                RestaurantReservationSystem.Add(reservationAt,
                    new Dictionary<int, Dictionary<int, List<TableLockModel>>>());
            }

            var restaurantReservations = RestaurantReservationSystem[reservationAt];
            if (!restaurantReservations.ContainsKey(reservation.RestaurantId))
            {
                restaurantReservations.Add(reservation.RestaurantId, new Dictionary<int, List<TableLockModel>>());
            }

            var tableReservations = restaurantReservations[reservation.RestaurantId];
            var tables = tableReservations[reservation.GuestAmount];
            var table = tables.FirstOrDefault(t => t.TableId == reservation.TableId);
                var timeOnly = TimeOnly.FromDateTime(reservation.CreatedAt);
            if (table is null)
            {
                tables.Add(new TableLockModel
                {
                    TableId = reservation.TableId, 
                    ArrivingTimes = new List<TimeOnly>() {timeOnly}
                });
            }
            else
            {
                table.ArrivingTimes!.Add(timeOnly);
            }
        }
        return true;
    }

    public async Task InitializeDeActiveRestaurant()
    {
        List<RestaurantDeActivationEntity> restaurantDeActivationTimes = await restaurantDeActivationDapperQueryRepository.GetAll();
        for (var i = 0; i < restaurantDeActivationTimes.Count; i++)
        {
            var item = restaurantDeActivationTimes[i];
            if (!RestaurantDeActivationTime.ContainsKey(item.RestaurantId))
            {
                var deactivationTimes = new List<Tuple<DateTime, DateTime>>();
                var deactivationTime = new Tuple<DateTime, DateTime>(item.StartAt, item.EndAt);
                deactivationTimes.Add(deactivationTime);
                RestaurantDeActivationTime.Add(item.RestaurantId, deactivationTimes);
            }
            else
                RestaurantDeActivationTime[item.RestaurantId].Add(new Tuple<DateTime, DateTime>(item.StartAt, item.EndAt));
        }
    }
    public async Task InitializeTables()
    {
        var allRestaurantAllTables = await tableDapperQueryRepository.GetAllActiveTable();
        var allEnvironmentAllTables = await tableDapperEnvironmentQuery.GetAllEnvironment();
        for (var i = 0; i < allRestaurantAllTables.Count; i++)
        {
            var tableInfo = allRestaurantAllTables[i];
            TableModels.Add(tableInfo.Id, new TableModel
            {
                MinPlace = tableInfo.MinPlace,
                MaxPlace = tableInfo.MaxPlace,
                TableNumber = tableInfo.TableNumber,
                RestaurantId = tableInfo.RestaurantId
            });
        }

        for (var i = 0; i < allEnvironmentAllTables.Count; i++)
        {
            var tableEnvironment = allEnvironmentAllTables[i];
            var tableEnvironments = TableModels[tableEnvironment.TableId].TableEnvironmentIds;
            var currentEnvironmentId = tableEnvironment.EnvironmentId;
            if (tableEnvironments is null)
                tableEnvironments= [currentEnvironmentId];
            else
            {
                var inserted = false;
                for (var j = 0; j < tableEnvironments.Count; j++)
                    if (tableEnvironments[j] > currentEnvironmentId)
                    {
                        tableEnvironments.Insert(j, currentEnvironmentId);
                        inserted = true;
                    }
                if(!inserted)
                    tableEnvironments.Add(currentEnvironmentId);
            }
        }

        allRestaurantAllTables = null;
        allEnvironmentAllTables = null;
        return;
    }
}

public class TableModel
{ 
    public int RestaurantId { get; init; }
    public int TableNumber { get; init; }
    public int MaxPlace { get; init; }
    public int MinPlace { get; init; }
    public List<int>? TableEnvironmentIds { get; set; }
}

public class TableLockModel
{
    public int TableId { get; set; }
    public bool IsBusyNow { get; set; }
    public DateTime LockDate { get; set; }
    public List<TimeOnly>? ArrivingTimes { get; set; }
    public SemaphoreSlim TableLock = new SemaphoreSlim(1, 1);
}
