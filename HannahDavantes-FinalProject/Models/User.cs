using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HannahDavantes_FinalProject.Models {
    public class User : IdentityUser {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "Province")]
        public string Province { get; set; }
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }


    }
}