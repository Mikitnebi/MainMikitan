using MainMikitan.Application.Services.CacheServices.GlobalRestaurantCacheService;
using MainMikitan.Domain.Models.Commons;
using MainMikitan.Domain.Requests.Customer.Feature.Reservation;
using MainMikitan.Domain.Templates;

namespace MainMikitan.Application.Features.Reservation.Command;

public class ReserveCommand(ReservationRequest request, int userId) : ICommand
{
    public readonly int UserId = userId;
    public readonly ReservationRequest Request = request;
}
