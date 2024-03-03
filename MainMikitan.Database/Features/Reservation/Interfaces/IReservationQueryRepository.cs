using MainMikitan.Domain.Models.Reservation;

namespace MainMikitan.Database.Features.Reservation.Interfaces;

public interface IReservationQueryRepository
{
    Task<List<ReservationEntity>> GetActiveReservationByCustomerId(int customerId, CancellationToken cancellationToken = default);
    Task<List<ReservationEntity>> GetActiveReservation(CancellationToken cancellationToken = default);
    Task<bool> HasAnyActiveReservationByCustomerId(int customerId, CancellationToken cancellationToken = default);
}