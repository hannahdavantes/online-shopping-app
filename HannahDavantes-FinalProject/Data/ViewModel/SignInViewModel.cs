using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HannahDavantes_FinalProject.Data.ViewModel {
    public class SignInViewModel {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email Address is Required")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is Required")]
        [MinLength(6, ErrorMessage = "Password must be atleast 6 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
