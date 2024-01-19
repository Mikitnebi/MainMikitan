using MainMikitan.Domain.Models.Reservation;

namespace MainMikitan.Database.Features.Reservation.Interfaces;

public interface IReservationCommandRepository
{
    Task<List<ReservationEntity>> GetActiveReservationByCustomerId(int customerId);
    Task<bool> HasAnyActiveReservationByCustomerId(int customerId);
}