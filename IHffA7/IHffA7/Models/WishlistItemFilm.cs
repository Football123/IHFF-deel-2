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
        public IList<WishlistItemFilm> WishlistItemsFilmList { get; set; }
        public WishlistItems WishlistItem { get; set; }
        public Activities Activiteit { get; set; }
        public FilmScreenings Filmvoorstelling { get; set; }
        public Films Film { get; set; }
        public Locations Location { get; set; }
        public WishlistItemFilm(int wishlistId)
        {
            IEnumerable<WishlistItems> films = wishListRepository.GetWishlistItems(wishlistId);
            WishlistItemsFilmList = new List<WishlistItemFilm>();
            foreach (WishlistItems i in films)
            {
                Activities activiteit;
                FilmScreenings voorstelling;
                Films film;
                Locations location;
                activiteit = wishListRepository.GetActiviteit(i.WishlistId);
                voorstelling = wishListRepository.GetFilmvoorstelling(activiteit.Id);
                film = wishListRepository.GetFilm(voorstelling.FilmId);
                //location = new Locations(99, "niet gelzen", "straat", "1554er", "laren", "gebouw");
                location = wishListRepository.GetLocation(activiteit.LocationId);
                WishlistItemsFilmList.Add(new WishlistItemFilm(i, activiteit,voorstelling,film,location));

            }
        }
        public WishlistItemFilm(WishlistItems wishlistItem, Activities activiteit, FilmScreenings filmvoorstelling,
            Films film, Locations location)
        {
            this.WishlistItem = wishlistItem;
            this.Activiteit = activiteit;
            this.Filmvoorstelling = filmvoorstelling;
            this.Film = film;
            this.Location = location;
        }
    }
}
