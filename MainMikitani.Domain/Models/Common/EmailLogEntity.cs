using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Domain.Models.Common
{
    public class EmailLogEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int UserTypeId { get; set; }
        public int EmailTypeId { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
