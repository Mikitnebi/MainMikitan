using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Domain.Models.Customer
{
    public class CustomerInfo
    {
        public bool GenderId { get; set; }
        public DateTime BirthDate { get; set; }
        public int NationalityId { get; set; }
        public int InterestCuisineId { get; set; }
        public int InterestMusicId { get; set; }
        public DateTime CreateAt { get; set; }
        public int StatusId { get; set; }
    }
}
