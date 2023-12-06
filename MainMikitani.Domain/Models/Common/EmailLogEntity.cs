using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Domain.Models.Common
{
    public class EmailLogEntity
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required] public string EmailAddress { get; set; } = null!;
        [Required]
        public int UserId { get; set; }
        [Required]
        public int UserTypeId { get; set; }
        [Required]
        public int EmailTypeId { get; set; }

        [Required] public string Data { get; set; } = null!;
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
