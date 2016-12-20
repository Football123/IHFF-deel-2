using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHffA7.Models
{
    public class WishlistViewModel
    {
        public IList<WishlistItemFilm> Films { get; set; }
        public IList<WishlistItemRestaurant> Restaurants { get; set; }

        public WishlistViewModel(IList<WishlistItemFilm> films, IList<WishlistItemRestaurant> restaurants)
        {
            this.Films = films;
            this.Restaurants = restaurants;
        }
    }
}
