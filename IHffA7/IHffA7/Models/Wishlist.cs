using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHffA7.Models
{
    public class Wishlist
    {
        public IEnumerable<WishlistItemFilm> Films { get; set; }


        public Wishlist(IEnumerable<WishlistItemFilm> films)
        {
            this.Films = films;
        }
    }
}
