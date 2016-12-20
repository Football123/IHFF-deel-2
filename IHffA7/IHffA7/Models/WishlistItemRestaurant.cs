using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IHffA7.Models.dbModels;
using IHffA7.Models.repositories;

namespace IHffA7.Models
{
    public class WishlistItemRestaurant
    {
        public WishlistItems WishlistItem { get; set; }
        public Activities Activiteit { get; set; }
        public Locations Location { get; set; }
        public decimal TotalPrice { get; set; }
        public Restaurants Restaurant { get; set; }

        public WishlistItemRestaurant(WishlistItems wishlistItem, Activities activiteit, Locations location,
             decimal totalPrice, Restaurants restaurant)
        {
            this.WishlistItem = wishlistItem;
            this.Activiteit = activiteit;
            this.Location = location;
            this.TotalPrice = totalPrice;
            this.Restaurant = restaurant;
        }
    }
}
