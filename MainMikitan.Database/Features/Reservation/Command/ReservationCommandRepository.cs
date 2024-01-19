using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.Reservation.Interfaces;
using MainMikitan.Domain.Models.Reservation;
using Microsoft.EntityFrameworkCore;

namespace MainMikitan.Database.Features.Reservation.Command;

public class ReservationCommandRepository(MikDbContext db) : IReservationCommandRepository
{
    public async Task<List<ReservationEntity>> GetActiveReservationByCustomerId(int customerId)
    {
        return await db.Reservations
            .Where(t => t.UserId == customerId)
            .Where(r => r.IsCompany == false)
            .Where(y => y.IsCanceled == false)
            .Where(c => c.Date > DateTime.Now)
            .ToListAsync();
    }

    public async Task<bool> HasAnyActiveReservationByCustomerId(int customerId)
    {
        return await db.Reservations
            .AnyAsync(t 
                => t.UserId == customerId 
                && t.IsCompany == false 
                && t.IsCanceled == false 
                && t.Date > DateTime.Now);
    }
}