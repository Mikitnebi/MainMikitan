using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Domain.Models.Restaurant
{
    public class RestaurantIntro
    {
        public string BusinessName { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string EmailAdress { get; set; }
        public int RegionId { get; set; }
    }
}
