using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IHffA7.Models.dbModels;
using IHffA7.Models.repositories;

namespace IHffA7.Models
{
    public class WishlistItemFilm
    {
        private WishlistRepository wishListRepository = new WishlistRepository();
        public IList<WishlistItemFilm> WishlistFilmList { get; set; }
        public WishlistItems WishlistItem { get; set; }
        public Activities Activiteit { get; set; }
        public FilmScreenings Filmvoorstelling { get; set; }
        public Films Film { get; set; }
        public Locations Location { get; set; }
        public decimal TotalPrice { get; set; }


        /*public void AddToWishlistFilmList(WishlistItemFilm film)
        {
            WishlistFilmList.Add(film);
        }*/
        public WishlistItemFilm(WishlistItems wishlistItem, Activities activiteit, FilmScreenings filmvoorstelling,
            Films film, Locations location, decimal totalPrice)
        {
            this.WishlistItem = wishlistItem;
            this.Activiteit = activiteit;
            this.Filmvoorstelling = filmvoorstelling;
            this.Film = film;
            this.Location = location;
            this.TotalPrice = totalPrice;
        }
    }
}
