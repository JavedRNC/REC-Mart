using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace User_Management_System_V2._0.Models
{
    public class RegisterModel
    {
        [Display(Name ="Full Name")]
        [Required]
        public string FullName { get; set; }

        [Display(Name = "User Name")]
        [Required]
        public string UserName { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(otherProperty:"Password", ErrorMessage = "Password does not match")]
        public string ConfirmPass { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [Compare(otherProperty: "Email", ErrorMessage = "Email does not match")]
        public string ConfirmEmail { get; set; }
    }
}