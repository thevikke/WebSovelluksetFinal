using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebSovelluksetFinal.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string SurName { get; set; }
        [Required]
        public string Adress { get; set; }
        [Required]
        public string BillingAddress { get; set; }
        [Required]
        public double SizeOfHouse { get; set; }
        [Required]
        public int HouseTypeID { get; set; }
        public HouseType HouseType { get; set; }
    }
}
