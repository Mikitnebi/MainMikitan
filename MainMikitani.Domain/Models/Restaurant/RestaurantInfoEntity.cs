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
        public double LocationX { get; init; }
        [Required]
        public double LocationY { get; init; }
        [Required]
        public int PriceTypeId { get; init; }
        [Required]
        public string Address { get; init; } = null!;
        [Required]
        public int RegionId { get; init; }
        public decimal Rate { get; init; }
        public int RateNumber { get; init; }
        [Required]
        public int BusinessTypeId { get; init; }
        [Required]
        public bool HasCoupe { get; init; }
        [Required]
        public bool HasTerrace { get; init; }
        [Required]
        public TimeOnly HallStartTime { get; init; }
        [Required]
        public TimeOnly HallEndTime { get; init; }
        [Required]
        public TimeOnly KitchenStartTime { get; init; }
        [Required]
        public TimeOnly KitchenEndTime { get; init; }
        [Required]
        public TimeOnly MusicStartTime { get; init; }
        [Required]
        public TimeOnly MusicEndTime { get; init; }
        [Required]
        public bool ForCorporateEvents { get; init; }
        public string? DescriptionGeo { get; init; }
        public string? DescriptionEng { get; init; }
        [Required]
        public int ActiveStatusId { get; init; }

        public int? UpdateUserId { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } 

    }
}
