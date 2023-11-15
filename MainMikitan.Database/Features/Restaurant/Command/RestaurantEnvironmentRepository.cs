using MainMikitan.Database.DbContext;
using MainMikitan.Database.Features.Restaurant.Interface;
using MainMikitan.Domain.Models.Restaurant;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Database.Features.Restaurant.Command {
    public class RestaurantEnvironmentRepository : IRestaurantEnvironmentRepository {
        public MikDbContext _mikDbContext;
        public RestaurantEnvironmentRepository(MikDbContext mikDbContext)
        {
            _mikDbContext = mikDbContext;          
        }

        public async Task<bool> Delete(int restaurantId) {
            var test = await _mikDbContext.RestaurantEnvironmentInfo.Where(r => r.RestaurantId == restaurantId).ToListAsync();
            if (test.Count == 0) return true;
            var result = await _mikDbContext.RestaurantEnvironmentInfo
                .Where(r => r.RestaurantId == restaurantId)
                .ExecuteDeleteAsync();
            return result > 0;
        }

        public async Task<bool> Add(List<int> environmentIds, int restaurantId) {
            foreach (var environment in environmentIds) {
                var restaurantEnvironmenEntity = new RestaurantEnvironmentInfoEntity {
                    CreatedAt = DateTime.Now,
                    EnvironmentId = environment,
                    RestaurantId = restaurantId,
                    IsActive = true
                };
                var result = await _mikDbContext.RestaurantEnvironmentInfo.AddAsync(restaurantEnvironmenEntity);
                if (result.Entity == null) return false;
            }
            return true;
        }
        public async Task<List<RestaurantEnvironmentInfoEntity>> Get(int restaurantId) {
            return await _mikDbContext.RestaurantEnvironmentInfo
                .Where(r => r.RestaurantId == restaurantId).ToListAsync();
        }
        public async Task<bool> SaveChanges() {
            var result = await _mikDbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}
