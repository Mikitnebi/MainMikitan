using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.Common.Multifunctional.Interface.Repository;
using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain.Models.Restaurant;
using MainMikitan.Domain.Models.Setting;
using Microsoft.Extensions.Options;

namespace MainMikitan.Database.Features.Restaurant.Command;

public class RestaurantBranchingCodeLogRepository(
    IOptions<ConnectionStringsOptions> connectionString,
    MikDbContext db)
    : IRestaurantBranchingCodeLogRepository
{
    private readonly ConnectionStringsOptions _connectionString = connectionString.Value;

    public async Task<int> Create(RestaurantBranchingCodeLogEntity model)
    {
            var result = await db.RestaurantBranchingCodeLogs.AddAsync(model);
            return result.Entity.Id;
    }
}