using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Domain.Requests.RestaurantRequests {
    public record RestaurantRegistrationFinalRequest {
        public DateTime UpdateDate { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public int ManagerId { get; set; }
        public string ManagerPhone { get; set; }
        public int BusinessTypeId { get; set; }
        public int CoupeQuantity { get; set; }
        public int TableQuantity { get; set; }
        public int TerrasseQuantity { get; set; }
        public int HallStartH { get; set; }
        public int HallEndH { get; set; }
        public int KitchenStartH { get; set; }
        public int KitchenEndH { get; set; }
        public int MusicStartH { get; set; }
        public int MusicEndH { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public bool ForCorporateEvents { get; set; }
    }
}
