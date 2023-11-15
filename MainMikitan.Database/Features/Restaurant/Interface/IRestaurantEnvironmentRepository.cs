using MainMikitan.Domain.Models.Restaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Database.Features.Restaurant.Interface {
    public interface IRestaurantEnvironmentRepository {
        public Task<bool> Delete(int restaurantId);
        public Task<bool> Add(List<int> environmentIds, int restaurantId);
        public Task<List<RestaurantEnvironmentInfoEntity>> Get(int restaurantId);
        public Task<bool> SaveChanges();

    }
}
