using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace User_Management_System_V2._0.Models.Account
{
    public class LoginModel
    {
        [Display(Name = "UserID:")]
        [Required(ErrorMessage = "Required")]
        public string UserName { get; set; }

        [Display(Name = "Password:")]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [Display(Name = "Remember Me:")]
        public bool RememberMe { get; set; }
    }
}