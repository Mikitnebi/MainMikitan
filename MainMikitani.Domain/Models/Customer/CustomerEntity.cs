using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Domain.Models.Customer
{
    public class CustomerEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public bool EmailConfirmation { get; set; }
        public string MobileNumber { get; set; }
        public bool MobileNumberConfirmation { get; set; }
        public string HashPassWord { get; set; }
        public DateTime CreatedAt { get; set; }
        public int StatusId { get; set; }
        public int GenderId { get; set; }
    }
}
