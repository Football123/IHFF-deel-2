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

        // niet meer ingebruikt
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

        public IList<WishlistItemFilm> GetAllWishlistFilms(List<SessionFilm> filmFromSession)
        {
            IList<WishlistItemFilm> wishlistItemsFilmList = new List<WishlistItemFilm>();
            foreach (SessionFilm movie in filmFromSession)
            {
                //"0" omdat het niet in de database hoeft te bestaan. bestaat alleen als de wishlist al in keer in de db is opgeslagen
                WishlistItems wishlistItem = new WishlistItems(0, 0, 0, movie.NumberOfpersones);
                Activities activiteit = ctx.Activity.SingleOrDefault(c => (c.Id == movie.ActivityId));
                FilmScreenings voorstelling = ctx.FilmScreening.SingleOrDefault(c => (c.ActivityId == activiteit.Id));
                Films film = ctx.Film.SingleOrDefault(c => (c.Id == voorstelling.FilmId));
                Locations location = ctx.Location.SingleOrDefault(c => (c.Id == activiteit.LocationId));
                decimal totalPriceFilm = wishlistItem.NumberOfPersons * activiteit.Price;
                //WishlistItemFilm wishlistFilm = new WishlistItemFilm(wishlistItem, activiteit, location, totalPriceFilm, voorstelling, film);
                wishlistItemsFilmList.Add(new WishlistItemFilm(wishlistItem, activiteit, location, totalPriceFilm, voorstelling, film));
            }
            return wishlistItemsFilmList;
        }
        public IList<WishlistItemRestaurant> GetAllWishlistRestaurants(List<SessionRestaurant> restaurantsFromSession)
        {
            IList<WishlistItemRestaurant> wishlistItemsRestaurantlist = new List<WishlistItemRestaurant>();
            foreach (SessionRestaurant reservation in restaurantsFromSession)
            {
                //"0" omdat het niet in de database hoeft te bestaan. bestaat alleen als de wishlist al in keer in de db is opgeslagen en dat weet je heir "nog" niet
                WishlistItems wishlistItem = new WishlistItems(0, 0, 0, reservation.NumberOfpersones);
                // er is geen eind tijd, hights is n.v.t. 
                // TODO PRIJS BINNENKRIJGEN! is nu maar even 111
                Activities activiteit = new Activities(0, reservation.Start, new DateTime(0), 111, reservation.LocationId, false);
                Restaurants restaurant = ctx.Restaurant.SingleOrDefault(c => (c.Id == reservation.RestaurantId));
                Locations location = ctx.Location.SingleOrDefault(c => (c.Id == activiteit.LocationId));
                decimal totalPriceFilm = wishlistItem.NumberOfPersons * activiteit.Price;
                wishlistItemsRestaurantlist.Add(new WishlistItemRestaurant(wishlistItem, activiteit, location, totalPriceFilm, restaurant));
            }
            return wishlistItemsRestaurantlist;
        }



        public IList<WishlistItemFilm> GetAllWishlistFilms(int wishlistId)
        {
            IEnumerable<WishlistItems> films = GetWishlistItems(wishlistId);
            IList<WishlistItemFilm> wishlistItemsFilmList = new List<WishlistItemFilm>();
            foreach (WishlistItems i in films)
            {
                Activities activiteit = GetActiviteit(i.WishlistId);
                Locations location = GetLocation(activiteit.LocationId);
                decimal totalPriceFilm = i.NumberOfPersons * activiteit.Price;
                FilmScreenings voorstelling = GetFilmvoorstelling(activiteit.Id);
                Films film = GetFilm(voorstelling.FilmId);
                wishlistItemsFilmList.Add(new WishlistItemFilm(i, activiteit, location,totalPriceFilm, voorstelling, film));
            }
            return (wishlistItemsFilmList);
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
