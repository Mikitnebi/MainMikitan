using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitani.Domain.Models.Customer
{
    public class CustomerAnalytics
    {
        public int TotalOrderQuantity { get; set; }
        public int AvarageMonthlyOrderQuantity { get; set; }
        public int TotalOfProfit { get; set; }
        public int AvarageMonthlyProfit { get; set; }
        public int AvarageApplicationView { get; set; }
    }
}