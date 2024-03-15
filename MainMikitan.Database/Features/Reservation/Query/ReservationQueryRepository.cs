using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.Reservation.Interfaces;
using MainMikitan.Domain.Models.Reservation;
using Microsoft.EntityFrameworkCore;

namespace MainMikitan.Database.Features.Reservation.Command;

public class ReservationQueryRepository(MikDbContext db) : IReservationQueryRepository
{
    public async Task<List<ReservationEntity>> GetActiveReservationByCustomerId(int customerId, CancellationToken cancellationToken = default)
    {
        return await db.Reservations
            .Where(t => t.UserId == customerId)
            .Where(r => r.IsCompany == false)
            .Where(y => y.IsCanceled == false)
            .Where(c => c.DateAt > DateTime.Now)
            .ToListAsync(cancellationToken: cancellationToken);
    }
    
    public async Task<List<ReservationEntity>> GetActiveReservation(CancellationToken cancellationToken = default)
    {
        return await db.Reservations
            .Where(r => r.IsCompany == false)
            .Where(y => y.IsCanceled == false)
            .Where(c => c.DateAt  > DateTime.Now)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<bool> HasAnyActiveReservationByCustomerId(int customerId, CancellationToken cancellationToken = default)
    {
        return await db.Reservations
            .AnyAsync(t 
                => t.UserId == customerId 
                && t.IsCompany == false 
                && t.IsCanceled == false 
                && t.DateAt  > DateTime.Now, cancellationToken: cancellationToken);
    }
}