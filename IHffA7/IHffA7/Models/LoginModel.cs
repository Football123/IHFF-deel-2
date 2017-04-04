using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IHffA7.Models
{
    public class LoginModel
    {
        //Emailadres:
        [Required(ErrorMessage = "Emailadres is verplicht")]
        [Display(Name = "Emailadres")]
        public string Emailadres { get; set; }

        //Wachtwoord:
        [Required(ErrorMessage = "Wachtwoord is verplicht")]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord")]
        public string Wachtwoord { get; set; }
    }
}