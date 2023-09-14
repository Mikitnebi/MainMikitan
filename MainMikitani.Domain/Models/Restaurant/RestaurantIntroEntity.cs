﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Domain.Models.Restaurant
{
    public class RestaurantIntroEntity
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        public int OtpVerificationId { get; set; }
        public string BusinessName { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }
        public bool EmailConfirmation { get; set; }
        public int RegionId { get; set; }
        public DateTime JoinedAt { get; set; }     
    }
}
