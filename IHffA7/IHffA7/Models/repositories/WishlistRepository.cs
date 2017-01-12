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

        //IhffA7Context ctx = new IhffA7Context(); //echt online dbd
        WhatsUp1617S_martinstinsGenerated ctx = new WhatsUp1617S_martinstinsGenerated();

        private IQueryable<WishlistItems> GetWishlistItems(int wishlistId)
        {
            return ctx.WishlistItems
                .Where(w => (w.wishlistId == wishlistId)); 
        }
        private IQueryable<Activities> GetActivity(int activityId)
        {
            var activiteit = ctx.Activities
                .Where(a => (a.id == activityId));
            return activiteit;
        }

        public IEnumerable<WishlistItemFilm> getFilmactivities(IList<SessionFilm> filmlist)
        {
            WishlistItemFilm wishlistItemFilm = new WishlistItemFilm();
            foreach (var activity in filmlist)
            {
                var test = GetActivity(activity.ActivityId); 
                var eagerloadActivities = GetActivity(activity.ActivityId)
                    .Include(s => s.Filmscreenings)
                    .Include(f => f.Filmscreenings.Select(ff => ff.Films))
                    .Include(r => r.Filmscreenings.Select(rr => rr.Rooms))
                    .Include(l => l.Filmscreenings.Select(ll => ll.Rooms.Locations));
                var activities = eagerloadActivities.Single();
                var screening = activities.Filmscreenings.Single();
                var film = screening.Films;
                var room = screening.Rooms;
                var location = room.Locations;
                WishlistItems wishlistItem = new WishlistItems();
                wishlistItem.numberOfPersons = activity.NumberOfpersones;
                wishlistItemFilm.Add(new WishlistItemFilm(wishlistItem, activities, screening, film, location, room, activity.NumberOfpersones * activities.price));
            }
            return wishlistItemFilm.WishlistFilmList;
        }

        

        //new gebruikte mthoene hier onder
        public IEnumerable<WishlistViewModel> GetActivities(List<WishlistSession> wishlistSessionList)
        {
            IList<WishlistViewModel> list = new List<WishlistViewModel>();
            foreach (WishlistSession item in wishlistSessionList)
            {
                var eagerloadActivity = GetActivity(item.ActivityId)
                .Include(f => f.Filmscreenings.Select(a => a.Films))
                .Include(f => f.Filmscreenings.Select(r => r.Rooms.Locations))
                .Include(f => f.Specialscreenings.Select(a => a.Specials))
                .Include(f => f.Specialscreenings.Select(r => r.Rooms.Locations))
                .Include(r => r.Restaurants.Select(rr => rr.Locations));
                list.Add(new WishlistViewModel(eagerloadActivity.Single(), item.NumberOfpersones));
            }
            list.OrderBy(order => order.Activity.typeActivity)
                .OrderBy(oderder2 => oderder2.Activity.startTime);
            return list;
        }
        public IEnumerable<WishlistViewModel> GetActivities(int wishlistId)
        {
            IList<WishlistViewModel> list = new List<WishlistViewModel>();
            //eagerloading to reduce queries to db to 1. without it lazyloading wil fire one by each loop
            var eagerloading = GetWishlistItems(wishlistId)
                .Include(a => a.Activities)
                .Include(f => f.Activities.Filmscreenings.Select(a => a.Films))
                .Include(f => f.Activities.Filmscreenings.Select(r => r.Rooms.Locations))
                .Include(s => s.Activities.Specialscreenings.Select(a => a.Specials))
                .Include(s => s.Activities.Specialscreenings.Select(r => r.Rooms.Locations))
                .Include(r=>r.Activities.Restaurants.Select(rr=>rr.Locations));

            foreach (WishlistItems wishitem in eagerloading)
            {
                list.Add(new WishlistViewModel(wishitem.Activities, wishitem.numberOfPersons));
            }
            return list;
        }

        public void SaveActivities(List<WishlistSession> wishlistSessionList)
        {
            Wishlists wislist = new Wishlists();
           
            wislist.paid = false;
            foreach (WishlistSession item in wishlistSessionList)
            {
                WishlistItems wishitems = new WishlistItems();
                //check of de activity daadwerkelijk in db staat
                Activities activiteit = GetActivity(item.ActivityId).Single();
                wishitems.activityId = activiteit.id;
                wishitems.numberOfPersons = item.NumberOfpersones;
                wislist.WishlistItems.Add(wishitems);
                wislist.totalPrice = wislist.totalPrice + activiteit.price;
            }
            ctx.Wishlists.Add(wislist);
            ctx.SaveChanges();
        }
        public void SaveFilmactivities(IList<SessionFilm> filmlist)
        {
            Wishlists wislist = new Wishlists();
            wislist.paid = false;
            ctx.Wishlists.Add(wislist);
            foreach (var activity in filmlist)
            {
                int activityId = GetActivity(activity.ActivityId).Select(a => a.id).SingleOrDefault();
                var wishslistitem = new WishlistItems();
                if (activityId != null)
                {
                    wishslistitem.activityId = activityId;
                    wishslistitem.wishlistId = wislist.id;
                }

                wislist.WishlistItems.Add(wishslistitem);
            }
            ctx.SaveChanges();

        }
    }
}
