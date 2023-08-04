using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Domain.Models.Common
{
    public class EmailEntity
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public int ReplacementQuantity { get; set; }
    }
}
