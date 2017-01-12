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

        public IEnumerable<WishlistItemFilm> GetActivities2(int wishlistId)
        {
            //IList<WishlistItemFilm> films = new List<WishlistItemFilm>();
            WishlistItemFilm wishlistItemFilm = new WishlistItemFilm();

            //eagerloading to reduce queries to db to 1. without it lazyloading wil fire one by each loop
            var eagerloading = GetWishlistItems(wishlistId, 1)
                .Include(a => a.Activities)
                .Include(s => s.Activities.Filmscreenings)
                .Include(f => f.Activities.Filmscreenings.Select(a => a.Films))
                .Include(f => f.Activities.Filmscreenings.Select(r => r.Rooms))
                .Include(f => f.Activities.Filmscreenings.Select(r => r.Rooms.Locations));

            foreach (WishlistItems wishitem in eagerloading)
            {
                //var test = ctx.Locations.Select(q => q.id == 1);
                var activiteit = wishitem.Activities;
                var screening = activiteit.Filmscreenings.Single();
                var film = screening.Films;
                var room = screening.Rooms;
                var location = room.Locations;

                /*var id = wishitem.Activities.id;
                var startTime = wishitem.Activities.startTime;
                DateTime? endTime = wishitem.Activities.endTime;
                var numberOfPersons = wishitem.numberOfPersons;
                var totalPrice = wishitem.Activities.price;
                var availableSeats = wishitem.Activities.Filmscreenings.Single().availableSeats;
                var locationname = wishitem.Activities.Filmscreenings.Single().Rooms.Locations.name;
                var roomName = wishitem.Activities.Filmscreenings.Single().Rooms.name;
                var title = wishitem.Activities.Filmscreenings.Single().Films.title;*/
                wishlistItemFilm.Add(new WishlistItemFilm(wishitem, activiteit, screening, film, location, room, wishitem.numberOfPersons * activiteit.price));
                //wishlistItemFilm.Add(new WishlistItemFilm(title, totalPrice, availableSeats, locationname, roomName, startTime, endTime, id, numberOfPersons, totalPrice, roomName));
            }
            return wishlistItemFilm.WishlistFilmList;
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
            }
            ctx.SaveChanges();

        }

        //new gebruikte mthoene hier onder
        public IEnumerable<WishlistViewModel> getActivities(List<WishlistSession> wishlistSessionList)
        {
            IList<WishlistViewModel> list = new List<WishlistViewModel>();
            foreach(WishlistSession item in wishlistSessionList)
            {
                var eagerloadActivity = GetActivity(item.ActivityId)
                    .Include(s => s.Filmscreenings)
                    .Include(f => f.Filmscreenings.Select(ff => ff.Films))
                    .Include(r => r.Filmscreenings.Select(rr => rr.Rooms))
                    .Include(l => l.Filmscreenings.Select(ll => ll.Rooms.Locations))
                    .Include(l => l.Specialscreenings.Select(ll => ll.Rooms.Locations));
                list.Add(new WishlistViewModel(eagerloadActivity.Single(),item.NumberOfpersones));
            }
            list.OrderBy(order => order.Activity.typeActivity)
                .OrderBy(oderder2 => oderder2.Activity.startTime);
            return list;
        }
        public IEnumerable<WishlistViewModel> GetActivities(int wishlistId)
        {
            IList<WishlistViewModel> list = new List<WishlistViewModel>();
            //eagerloading to reduce queries to db to 1. without it lazyloading wil fire one by each loop
            var eagerloading = GetWishlistItems(wishlistId, 1)
                .Include(a => a.Activities)
                .Include(s => s.Activities.Filmscreenings)
                .Include(f => f.Activities.Filmscreenings.Select(a => a.Films))
                .Include(f => f.Activities.Filmscreenings.Select(r => r.Rooms))
                .Include(f => f.Activities.Filmscreenings.Select(r => r.Rooms.Locations));

            foreach (WishlistItems wishitem in eagerloading)
            {
                list.Add(new WishlistViewModel(eagerloading.Single().Activities, wishitem.numberOfPersons));
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
                wishitems.numberOfPersons = 20;
                wislist.WishlistItems.Add(wishitems);
                wislist.totalPrice = wislist.totalPrice + activiteit.price;
            }
            var test = wislist;
            ctx.SaveChanges();
        }
    }
}
