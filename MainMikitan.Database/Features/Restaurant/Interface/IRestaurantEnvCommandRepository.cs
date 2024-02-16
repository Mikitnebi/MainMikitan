using MainMikitan.Domain.Models.Restaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Database.Features.Restaurant.Interface {
    public interface IRestaurantEnvCommandRepository {
        public Task<bool> Delete(int restaurantId, CancellationToken cancellationToken = default);
        public Task<bool> Add(IEnumerable<int> environmentIds, int restaurantId,CancellationToken cancellationToken = default);
        public Task<bool> SaveChanges(CancellationToken cancellationToken = default);

    }
}
