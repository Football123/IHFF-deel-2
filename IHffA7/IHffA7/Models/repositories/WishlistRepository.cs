using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IHffA7.Models.repositories
{
    class WishlistRepository
    {
        //IhffA7Context ctx = new IhffA7Context(); //echt online db
        WhatsUp1617S_martinstinsGenerated ctx = new WhatsUp1617S_martinstinsGenerated(); //test offline db

        private IQueryable<WishlistItems> GetWishlistItems(int wishlistId)
        {
            return ctx.WishlistItems.Where(w => (w.wishlistId == wishlistId));
        }

        public IEnumerable<WishlistItemFilm> GetAllFilmActivies(int wishlistId)
        {
            IList<WishlistItemFilm> films = new List<WishlistItemFilm>();
            WishlistItemFilm wishlistItemFilm = new WishlistItemFilm();
            foreach (WishlistItems wishitem in GetWishlistItems(wishlistId))
            {

                var activiteit = wishitem.Activities;
                if (activiteit.TypeActivity == 1)
                {
                    var screening = activiteit.Filmscreenings.Single();
                    var film = screening.Films;
                    var room = screening.Rooms;
                    var location = room.Locations;
                    // beter om de lijstjes van de view model te vullen!
                    wishlistItemFilm.Add(new WishlistItemFilm(wishitem, activiteit, screening, film, location, room, wishitem.numberOfPersons * activiteit.price));
                }
                if (activiteit.TypeActivity == 2)
                {
                    var screening = activiteit.Specialscreenings.Single();
                    var special = screening.Specials;
                    var room = screening.Rooms;
                    var location = room.Locations;
                    //add to specials list
                }
                if (activiteit.TypeActivity == 3)
                {
                    var restaurant = activiteit.Restaurants.Single();
                    var location = restaurant.Locations;
                    //add to restaurants list
                }
            }
            return wishlistItemFilm.WishlistFilmList;
        }

        public IQueryable<Activities> GetAllFilmActiviesNew(int wishlistId)
        {
            //aanmaken
            var filmActivities = GetWishlistItems(wishlistId)
                .Select(w => (w.Activities))
                .Where(a => (a.Filmscreenings.Count()==1));
            return filmActivities;
        }


    }
}
