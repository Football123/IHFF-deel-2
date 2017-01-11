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
        public Restaurants Restaurant { get; set; }
        public bool Highlight { get; set; }
        public decimal Price { get; set; }
        public RestaurantsFormModel()
        {
                
        }
        public RestaurantsFormModel(Restaurants restaurant, bool highlight, decimal price)
        {
            this.Restaurant = restaurant;
            this.Highlight = highlight;
            this.Price = price;
        }
    }

}
