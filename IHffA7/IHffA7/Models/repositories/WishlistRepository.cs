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
        public IEnumerable<Locations> GetAllLocations()
        {
            return ctx.Location.ToList();
        }
        public Locations GetLocation(int locationId)
        {
            return ctx.Location.SingleOrDefault(c => (c.Id == locationId));
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
        //niet meer ingebruik
        public IEnumerable<FilmScreenings> GetFilmvoorstellingen(int activiteitId)
        {
            return ctx.FilmScreening.Where(c => (c.ActivityId == activiteitId)).ToList();
        }
        public Restaurants GetRestaurant(int activiteitId)
        {
            return ctx.Restaurant.SingleOrDefault(c => (c.Id == activiteitId));
        }

        public IEnumerable<Restaurants> GetRestaurants(int activiteitId)
        {
            return ctx.Restaurant.Where(c => (c.Id == activiteitId)).ToList();
        }


        //untested yet
        public IList<WishlistItemFilm> GetAllWishlistFilms(List<SessionFilm> filmFromSession)
        {
            IList<WishlistItemFilm> wishlistItemsFilmList = new List<WishlistItemFilm>();
            foreach (SessionFilm movie in filmFromSession)
            {
                //"0" omdat het niet in de database hoeft te bestaan. bestaat alleen als de wishlist al in keer in de db is opgeslagen
                WishlistItems wishlistItem = new WishlistItems(0, 0, 0, movie.NumberOfpersones);
                Activities activiteit = ctx.Activity.SingleOrDefault(c => (c.Id == movie.ActivityId));
                FilmScreenings voorstelling = GetFilmvoorstelling(activiteit.Id);
                Films film = GetFilm(voorstelling.FilmId);
                Locations location = GetLocation(activiteit.LocationId);
                decimal totalPriceFilm = wishlistItem.NumberOfPersons * activiteit.Price;
                //WishlistItemFilm wishlistFilm = new WishlistItemFilm(wishlistItem, activiteit, location, totalPriceFilm, voorstelling, film);
                wishlistItemsFilmList.Add(new WishlistItemFilm(wishlistItem, activiteit, location, totalPriceFilm, voorstelling, film));
            }
            return wishlistItemsFilmList;
        }

        //untested yet
        public IList<WishlistItemRestaurant> GetAllWishlistRestaurants(List<SessionRestaurant> restaurantsFromSession)
        {
            IList<WishlistItemRestaurant> wishlistItemsRestaurantlist = new List<WishlistItemRestaurant>();
            foreach (SessionRestaurant SesRestaurant in restaurantsFromSession)
            {
                //"0" omdat het niet in de database hoeft te bestaan. bestaat alleen als de wishlist al in keer in de db is opgeslagen en dat weet je heir "nog" niet
                WishlistItems wishlistItem = new WishlistItems(0, 0, 0, SesRestaurant.NumberOfpersones);
                // er is geen eind tijd, hights is n.v.t. 
                // TODO PRIJS BINNENKRIJGEN! is nu maar even 111
                Activities activiteit = new Activities(0, SesRestaurant.Start, new DateTime(0), 111, SesRestaurant.LocationId, false);
         //Misschien toch de activity id binnenkrijgen?!?
                Restaurants restaurant = ctx.Restaurant.SingleOrDefault(c => (c.Id == SesRestaurant.RestaurantId));
                Locations location = ctx.Location.SingleOrDefault(c => (c.Id == activiteit.LocationId));
                decimal totalPrice = wishlistItem.NumberOfPersons * activiteit.Price;
                wishlistItemsRestaurantlist.Add(new WishlistItemRestaurant(wishlistItem, activiteit, location, totalPrice, restaurant));
            }
            return wishlistItemsRestaurantlist;
        }



        public IList<WishlistItemFilm> GetAllWishlistFilms(int wishlistId)
        {
            IEnumerable<WishlistItems> wishlistItems = GetWishlistItems(wishlistId);
            IList<WishlistItemFilm> wishlistItemsFilmList = new List<WishlistItemFilm>();
            foreach (WishlistItems i in wishlistItems)
            {
                FilmScreenings filmvoorstelling = GetFilmvoorstelling(i.ActivityId);
                if (filmvoorstelling != null)
                {
                    Activities activiteit = GetActiviteit(i.WishlistId);
                    Locations location = GetLocation(activiteit.LocationId);
                    decimal totalPriceFilm = i.NumberOfPersons * activiteit.Price;
                    Films film = GetFilm(filmvoorstelling.FilmId);
                    wishlistItemsFilmList.Add(new WishlistItemFilm(i, activiteit, location, totalPriceFilm, filmvoorstelling, film));

                }
            }
            return (wishlistItemsFilmList);
        }

        public IList<WishlistItemRestaurant> GetAllWishlistRestaurants(int wishlistId)
        {
            IEnumerable<WishlistItems> restaurants = GetWishlistItems(wishlistId);
            IList<WishlistItemRestaurant> wislistsItemsRestaurantList = new List<WishlistItemRestaurant>();
            foreach (WishlistItems i in restaurants)
            {
                Restaurants restaurant = GetRestaurant(i.ActivityId);
                if (restaurant != null)
                {
                    Activities activiteit = GetActiviteit(i.ActivityId);
                    Locations location = GetLocation(activiteit.LocationId);
                    decimal totalPriceRestaurant = i.NumberOfPersons * activiteit.Price;
                    wislistsItemsRestaurantList.Add(new WishlistItemRestaurant(i, activiteit, location, totalPriceRestaurant, restaurant));
                }
            }
            return (wislistsItemsRestaurantList);
        }



        //untested! geen geod idee zie comment hier onder decreciated
        /*public IList<WishlistItemFilm> GetAllWishlistFilms(IList<int> wishlistItems)
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
        }*/
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
