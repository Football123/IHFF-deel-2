using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IHffA7.Models
{
    public class LoginModel
    {
        //Inlognaam:
        [Required(ErrorMessage = "Inlognaam is verplicht")]
        [Display(Name = "Inlognaam")]
        public string Inlognaam { get; set; }

        //Wachtwoord:
        [Required(ErrorMessage = "Wachtwoord is verplicht")]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord")]
        public string Wachtwoord { get; set; }
    }
}