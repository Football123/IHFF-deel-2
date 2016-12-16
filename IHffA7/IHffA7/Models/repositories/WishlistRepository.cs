using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IHffA7.Models.dbModels;

namespace IHffA7.Models.repositories
{
    class WishlistRepository
    {
        IhffA7Context ctx = new IhffA7Context();

        public IEnumerable<WishlistItems>GetWishlistItems(int wishlistId)
        {
            return ctx.WishlistItem.Where(c => (c.WishlistId == wishlistId)).ToList();
        }

        public WishlistItems GetWishlistItem(int wishlistItemId)
        {
            return ctx.WishlistItem.SingleOrDefault(c => (c.WishlistId == wishlistItemId));
        }
        public IEnumerable<Locations> GetLocations()
        {
            return ctx.Location.ToList();
        }
        public Locations GetLocation(int id)
        {
            return ctx.Location.SingleOrDefault(c => (c.Id == id));
        }
        public Activities GetActiviteit(int wishlistItem)
        {
            return ctx.Activity.SingleOrDefault(c => (c.Id == wishlistItem));
        }

        public FilmScreenings GetFilmvoorstelling(int activiteitId)
        {
            return ctx.FilmScreening.SingleOrDefault(c => (c.ActivityId == activiteitId));
        }
        public Films GetFilm(int filmId)
        {
            return ctx.Film.SingleOrDefault(c => (c.Id == filmId));
        }
        /*
        public WishlistItemFilm GetWishlistItemFilm(int numberOfpersones, int activityId, DateTime start, DateTime end)
        {

        }*/

        public IList<WishlistItemFilm> GetAllWishlistFilms(int wishlistId)
        {
            IEnumerable<WishlistItems> films = GetWishlistItems(wishlistId);
            IList<WishlistItemFilm> WishlistItemsFilmList = new List<WishlistItemFilm>();
            foreach (WishlistItems i in films)
            {
                Activities activiteit = GetActiviteit(i.WishlistId);
                Locations location = GetLocation(activiteit.LocationId);
                decimal totalPriceFilm = i.NumberOfPersons * activiteit.Price;
                FilmScreenings voorstelling = GetFilmvoorstelling(activiteit.Id);
                Films film = GetFilm(voorstelling.FilmId);
                WishlistItemsFilmList.Add(new WishlistItemFilm(i, activiteit, location,totalPriceFilm, voorstelling, film));
            }
            return (WishlistItemsFilmList);
        }

        //untested! geen geod idee zie comment hier onder decreciated
        public IList<WishlistItemFilm> GetAllWishlistFilms(IList<int> wishlistItems)
        {
            IList<WishlistItemFilm> WishlistItemsFilmList = new List<WishlistItemFilm>();
            foreach (int i in wishlistItems)
            {
                WishlistItems wishlistItem = GetWishlistItem(i);
                Activities activiteit = GetActiviteit(wishlistItem.WishlistId);
                FilmScreenings voorstelling = GetFilmvoorstelling(activiteit.Id);
                Films film = GetFilm(voorstelling.FilmId);
                Locations location = GetLocation(activiteit.LocationId);
                decimal totalPriceFilm = wishlistItem.NumberOfPersons * activiteit.Price;
                WishlistItemsFilmList.Add(new WishlistItemFilm(wishlistItem, activiteit, location, totalPriceFilm, voorstelling, film));
            }
            return WishlistItemsFilmList;
        }
        //untested! unfinished! en gaat niet werken, want heel wishlistitem object moet worden opgeslagen in seseion!
        public void SaveWishlist(IList<int> wishlistItems)
        {
           /* Wishlists l = new Wishlists(null, 0, false);
            foreach (int i in wishlistItems)
            {
                ctx.WishlistItem.Add(GetWishlistItem(i));
            }
            ctx.SaveChanges();*/
        }

    }
}
