using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Domain.Models.Common
{
    public class OtpLogIntroEntity
    {
         public int Id { get; set; }
        public string Otp { get; set; }
        public string EmailAdress { get; set; }
        public string MobieNumber { get; set; }
        public DateTime CreateAt { get; set; }
        public int ValidationTime { get; set; }
        public int NumberOfTrials { get; set; }
        public bool NumberOfTrialsIsRequeired { get; set; }
    }
}
