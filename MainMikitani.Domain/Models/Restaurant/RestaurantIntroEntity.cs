using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Domain.Models.Restaurant
{
    public class RestaurantIntroEntity
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Required]
        public string EmailAddress { get; set; } = null!;
        [Required]
        public bool EmailConfirmation { get; set; }
        [Required]
        public int RestaurantOtpVerificationId { get; set; }
        [Required]
        public string BusinessNameGeo { get; set; } = null!;
        [Required]
        public string BusinessNameEng { get; set; } = null!;
        [Required]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        public int RegionId { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }     
        public int ParentId { get; set; }
        public int ParentConfirmation { get; set; }
    }
}
