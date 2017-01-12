using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IHffA7.Models
{
    public class RestaurantOverviewModel
    {
        public int Id { get; set; }
        [Display(Name = "Id")]
        public string Name { get; set; }
        public string DescriptionNL { get; set; }
        public string Street { get; set; }
        public int StreetNumber { get; set; }
        public string ZipCode { get; set; }
        public string Town { get; set; }
        public DateTime LunchStart { get; set; }
        public DateTime LunchEnd { get; set; }
        public DateTime DinnerStart { get; set; }
        public DateTime DinnerEnd { get; set; }
        public string RestaurantLogo { get; set; }

        [Required(ErrorMessage = "Kies een datum")]
            public DateTime Datum { get; set; }
        [Required(ErrorMessage = "Kies een tijd")]
            public TimeSpan Tijd { get; set; }
        [Required(ErrorMessage = "Kies het aantal personen")]
            public int AantalPersonen { get; set; }
    }
}
