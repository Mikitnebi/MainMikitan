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
        public decimal LocationX { get; set; }
        [Required]
        public decimal LocationY { get; set; }
        [Required]
        public int PriceTypeId { get; set; }
        [Required]
        public string Address { get; set; } = null!;

        [Required] public string AddressEng { get; set; } = null!;
        [Required]
        public int RegionId { get; set; }
        public decimal Rate { get; set; }
        public int RateNumber { get; set; }
        [Required]
        public int BusinessTypeId { get; set; }
        [Required]
        public bool HasCoupe { get; set; }
        [Required]
        public bool HasTerrace { get; set; }
        [Required]
        public TimeOnly HallStartTime { get; set; }
        [Required]
        public TimeOnly HallEndTime { get; set; }
        [Required]
        public TimeOnly KitchenStartTime { get; set; }
        [Required]
        public TimeOnly KitchenEndTime { get; set; }
        [Required]
        public TimeOnly MusicStartTime { get; set; }
        [Required]
        public TimeOnly MusicEndTime { get; set; }
        [Required]
        public bool ForCorporateEvents { get; set; }
        public string? DescriptionGeo { get; set; }
        public string? DescriptionEng { get; set; }
        [Required]
        public int ActiveStatusId { get; set; }

        public int? UpdateUserId { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
