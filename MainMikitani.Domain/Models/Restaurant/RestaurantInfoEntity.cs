using MainMikitan.Domain.Models.MultifunctionalQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static MainMikitan.Domain.Enums;

namespace MainMikitan.Domain.Models.Restaurant
{
    public class RestaurantInfoEntity : MultifunctionalQueryMainModel
    {
        //
        public double LocationX { get; set; }
        public double LocationY { get; set; }
        public string Address { get; set; } = null!;
        public int ManagerId { get; set; }
        public int OperatorId { get; set; }
        public int BusinessTypeId { get; set; }
        public int CoupeQuantity { get; set; }
        public int TableQuantity { get; set; }
        public int TerraceQuantity { get; set; }
        public short HallStartHour { get; set; }
        public short HallEndHour { get; set; }
        public short HallStartMinute { get; set; }
        public short HallEndMinute { get; set; }
        public short KitchenStartHour { get; set; }
        public short KitchenEndHour { get; set; }
        public short KitchenStartMinute { get; set; }
        public short KitchenEndMinute { get; set; }
        public short MusicStartHour { get; set; }
        public short MusicEndHour { get; set; }
        public short MusicStartMinute { get; set; }
        public short MusicEndMinute { get; set; }
        public bool ForCorporateEvents { get; set; }
        public string? Description { get; set; }
        public int ActiveStatusId { get; set; }
        public bool TwoStepAuth { get; set; }
        public int UpdateUserId { get; set; }
        public DateTime UpdateAt { get; set; }
        public DateTime CreateAt { get; set; }

    }
}
