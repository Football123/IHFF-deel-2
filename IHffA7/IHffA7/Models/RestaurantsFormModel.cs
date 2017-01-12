using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace IHffA7.Models
{
    public class RestaurantsFormModel
    {
        public string descriptionNL { get; set; }
        public string descriptionEN { get; set; }
        public DateTime lunchStart { get; set; }
        public DateTime lunchEnd { get; set; }
        public DateTime dinnerStart { get; set; }
        public DateTime dinnerEnd { get; set; }
        public string restaurantLogo { get; set; }
        public int locationId { get; set; }
        public bool Highlight { get; set; }
        public decimal Price { get; set; }
        public RestaurantsFormModel()
        {
        }
    }

}
