using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MainMikitan.Domain.Models.Restaurant
{
    public class RestaurantEntity
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [Length(2,50)]
        public string BusinessNameGeo { get; set; } = null!;

        [Required] [Length(2, 50)] 
        public string BusinessNameEng { get; set; } = null!;
        
        [Required] [Length(2, 50)] 
        public string OfficialEmail { get; set; } = null!;
        
        [Required]
        public bool EmailConfirmation { get; set; } 
        
        [Required]
        public int StatusId { get; set; }
        public DateTime CreatedAt { get;set; }
    }
}
