using MainMikitan.Database.Features.Reservation.Dapper.Interface;
using MainMikitan.Database.Features.Reservation.Interfaces;
using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Database.Features.Table.Interface;
using MainMikitan.Domain.Models.Restaurant.TableManagement;

namespace MainMikitan.Application.Services.CacheServices.GlobalRestaurantCacheService;

public class GlobalRestaurantCacheService
{/*
    ITableDapperQueryRepository tableDapperQueryRepository,
    ITableDapperEnvironmentQueryRepository tableDapperEnvironmentQuery,
    IReservationDapperQueryRepository reservationDapperQueryRepository,
    //IRestauranActivationDapperQueryRepository restauranActivationDapperQueryRepository
    ) : IGlobalRestaurantCacheService
{
    public static Dictionary<int, TableModel> Tables = new Dictionary<int, TableModel>(20);
    public static Dictionary<DateOnly,
        Dictionary<int, Dictionary<int, int>>> RestaurantReservationSystem { get; set; } =
        new Dictionary<DateOnly, Dictionary<int, Dictionary<int, int>>>(21);

    public async Task<bool> Initilize()
    {
        await InitializeTables();
        var currentDateTime = DateTime.Now;
        //var activeReservations = await reservationDapperQueryRepository.GetActiveReservation();

        for (var i = 0; i <= 14; i++)
        {
            currentDateTime = currentDateTime.AddDays(i);
            var currentDateTimeOnly = DateOnly.FromDateTime(currentDateTime);
            RestaurantReservationSystem.Add(currentDateTimeOnly, new Dictionary<int, Dictionary<int, int>>(20));
        }
        return true;
    }

    public async Task InitializeAllActiveRestaurant()
    {
       // var allActiveRestaurant = await restauranActivationDapperQueryRepository.GetAllInAcitiveOnlyIdsByDateOnly();
      //  var
       // for(int i = 0 ; i < allActiveRestaurant.Count; i++)

    }
    private async Task InitializeTables()
    {
        var allRestaurantAllTables = await tableDapperQueryRepository.GetAllTable();
        var allEnvironmentAllTables = await tableDapperEnvironmentQuery.GetAllEnvironment();
        for (var i = 0; i < allRestaurantAllTables.Count; i++)
        {
            var tableInfo = allRestaurantAllTables[i];
            Tables.Add(tableInfo.Id, new TableModel
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
            var tableEnvironments = Tables[tableEnvironment.TableId].TableEnvironmentIds;
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
    }*/
}


public class TableModel
{ 
    public int RestaurantId { get; init; }
    public int TableNumber { get; init; }
    public int MaxPlace { get; init; }
    public int MinPlace { get; init; }
    public List<int>? TableEnvironmentIds { get; set; }
    public bool IsBusyNow { get; set; }

    public List<TimeOnly>? ArrivingTimes { get; set; }
    public DateTime LockDate { get; set; }
    public SemaphoreSlim TableLock= new SemaphoreSlim(1, 1); 
}
