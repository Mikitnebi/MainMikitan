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
        public int UserTypeId { get; set; } 
        public string Otp { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserTypeId { get; set; }
        public int StatusId { get; set; }
        public int ValidationTime { get; set; }
        public int NumberOfTrials { get; set; }
        public bool NumberOfTrialsIsRequired { get; set; }
    }
}
