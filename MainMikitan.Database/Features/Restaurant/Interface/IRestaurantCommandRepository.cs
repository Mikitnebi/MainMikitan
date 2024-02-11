using MainMikitan.Domain.Models.Restaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Database.Features.Restaurant.Interface
{
    public interface IRestaurantCommandRepository
    {
        Task<bool> Create(RestaurantEntity restaurant, CancellationToken cancellationToken = default);
        Task<bool> SaveChanges(CancellationToken cancellationToken = default);
    }
}
