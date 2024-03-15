using MainMikitan.Domain.Models.Reservation;

namespace MainMikitan.Database.Features.Reservation.Dapper.Interface;

public interface IReservationDapperQueryRepository
{
    Task<List<ReservationEntity>?> GetActiveReservation();
}