using MainMikitan.Domain.Models.Restaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Domain.Interfaces.Restaurant {
    public interface IRestaurantIntroQueryRepository {
        Task<RestaurantIntroEntity> GetNonVerifiedByEmail(string email);
    }
}
