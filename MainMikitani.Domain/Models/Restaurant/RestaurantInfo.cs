using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitani.Domain.Models.Restaurant
{
    public class RestaurantInfo
    {
        public DateTime DateJoined { get; set; }
        public string Location { get; set; }
        public string Adress { get; set; }
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
        public bool MusicType { get; set; }
        public int MusicStartH { get; set; }
        public int MusicEndH { get; set; }
        public string Description { get; set; }
        public int KitchenTypeId { get; set; }
        public int PlaceQuantityId { get; set; }
        public bool BackYard { get; set; }
        public bool Bar { get; set; }
        public bool Active { get; set; }
        public bool ForCorporateEvents { get; set; }
        public bool SmokingSpace { get; set; }
        public bool SmokingInTheHall { get; set; }




    }
}
