using MainMikitan.Domain.Models.Restaurant.TableManagement;

namespace MainMikitan.Database.Features.Restaurant.Interface;

public interface IRestaurantRepository
{
    Task<List<TableInfoEntity>?> GetAllActiveTable(CancellationToken cancellationToken = default);
}