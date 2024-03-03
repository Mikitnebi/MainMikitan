using MainMikitan.Domain.Models.Restaurant.TableManagement;

namespace MainMikitan.Database.Features.Reservation.Dapper.Interface;

public interface ITableDapperEnvironmentQueryRepository
{ 
    Task<List<TableEnvironmentEntity>> GetAllEnvironment();
}