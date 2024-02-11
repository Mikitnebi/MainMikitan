using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MainMikitan.Domain.Models.Restaurant
{
    public class RestaurantInfoEntity 
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int RestaurantId { get; set; }
        [Required]
        public double LocationX { get; set; }
        [Required]
        public double LocationY { get; set; }
        [Required]
        public int PriceTypeId { get; set; }
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        public int BusinessTypeId { get; set; }
        [Required]
        public bool HasCoupe { get; set; }
        [Required]
        public bool HasTerrace { get; set; }
        [Required]
        public short HallStartHour { get; set; }
        [Required]
        public short HallEndHour { get; set; }
        [Required]
        public short HallStartMinute { get; set; }
        [Required]
        public short HallEndMinute { get; set; }
        [Required]
        public short KitchenStartHour { get; set; }
        [Required]
        public short KitchenEndHour { get; set; }
        [Required]
        public short KitchenStartMinute { get; set; }
        [Required]
        public short KitchenEndMinute { get; set; }
        [Required]
        public short MusicStartHour { get; set; }
        [Required]
        public short MusicEndHour { get; set; }
        [Required]
        public short MusicStartMinute { get; set; }
        [Required]
        public short MusicEndMinute { get; set; }
        [Required]
        public bool ForCorporateEvents { get; set; }
        public string? DescriptionGeo { get; set; }
        public string? DescriptionEng { get; set; }
        [Required]
        public int ActiveStatusId { get; set; }

        public int? UpdateUserId { get; set; }
        public DateTime? UpdateAt { get; set; }
        [Required]
        public DateTime CreateAt { get; set; } 

    }
}
