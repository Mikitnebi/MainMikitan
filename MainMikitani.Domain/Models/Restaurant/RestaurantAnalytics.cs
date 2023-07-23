using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitani.Domain.Models.Restaurant
{
    public class RestaurantAnalytics
    {
        public int Rating { get; set; }
        public int OurProfitSum { get; set; }
        public int OurProfitMonthly { get; set; }
        public int RestaurantProfitSum { get; set; }
        public int RestaurantProfitMonthly { get; set; }

        public int ViewsPerDay { get; set; }
    }
}
