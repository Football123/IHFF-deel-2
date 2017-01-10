using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Net;


namespace IHffA7.Models.repositories
{
    class WishlistRepository
    {

        //IhffA7Context ctx = new IhffA7Context(); //echt online db
        WhatsUp1617S_martinstinsGenerated ctx = new WhatsUp1617S_martinstinsGenerated();

        private IQueryable<WishlistItems> GetWishlistItems(int wishlistId, int typeActivity)
        {
            return  ctx.WishlistItems
                .Where(w => (w.wishlistId == wishlistId))
                .Where(x => x.Activities.typeActivity == typeActivity);
        }
        private IQueryable<Activities> GetActivity(int activityId)
        {
            var activiteit = ctx.Activities
                .Where(a => (a.id == activityId));
            return activiteit;
        }

        public IEnumerable<WishlistItemFilm> getFilmactivities(int wishlistId)
        {
            //IList<WishlistItemFilm> films = new List<WishlistItemFilm>();
            WishlistItemFilm wishlistItemFilm = new WishlistItemFilm();

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

        public void SaveFilmactivities(IList<SessionFilm> filmlist)
        {
            Wishlists wislist = new Wishlists();
            wislist.paid = false;
            ctx.Wishlists.Add(wislist);
            foreach (var activity in filmlist)
            {
                int activityId = GetActivity(activity.ActivityId).Select(a=>a.id).SingleOrDefault();
                var wishslistitem = new WishlistItems();
                if (activityId != null)
                {
                    wishslistitem.activityId = activityId;
                    wishslistitem.wishlistId = wislist.id;
                }

                wislist.WishlistItems.Add(wishslistitem);
                ctx.SaveChanges();
            }
            
        }

    }
}
