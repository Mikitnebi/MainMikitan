using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Domain.Models.Company
{
    public class LegalEntityEntity
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Length(2, 50)]
        public string BusinessNameGeo { get; set; } = null!;

        [Required]
        [Length(2, 50)]
        public string BusinessNameEng { get; set; } = null!;

        [Required]
        [Length(2, 50)]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [Length(2, 50)]
        public string LegalEntityRepresentativeNameGeo { get; set; } = null;
        [Required]
        [Length(2, 50)]
        public string LegalEntityRepresentativeNameEng { get; set; } = null;
        [Length(5, 20)]
        public string UsernameHash { get; set; } = null;
        public string PasswordHash { get; set; } = null;
        public string PhoneNumber { get; set; } = null;
        public DateTime CreatedAt { get; set; }
        public int StatusId { get; set; }
    }
}
