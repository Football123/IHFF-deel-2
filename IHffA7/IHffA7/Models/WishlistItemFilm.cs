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
        public WishlistItemFilm(int wishlistId)
        {
            IEnumerable<WishlistItems> films = wishListRepository.GetWishlistItems(wishlistId);
            foreach(WishlistItems i in films)
            {
                Activities activiteit;
                FilmScreenings voorstelling;
                Films film;
                activiteit = wishListRepository.GetActiviteit(i.WishlistId);
                voorstelling = wishListRepository.GetFilmvoorstelling(activiteit.Id);
                film = wishListRepository.GetFilm(voorstelling.FilmId);
                WishlistItemsFilmList.Add(new WishlistItemFilm(i, activiteit,voorstelling,film));

            }
        }
        public WishlistItemFilm(WishlistItems wishlistItem, Activities activiteit, FilmScreenings filmvoorstelling,
            Films film)
        {
            this.WishlistItem = wishlistItem;
            this.Activiteit = activiteit;
            this.Filmvoorstelling = filmvoorstelling;
            this.Film = film;
        }
    }
}
