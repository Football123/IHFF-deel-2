﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Net;
using System.Web.Helpers;

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

        public Reservations getReservation(int wishlistId)
        {
            return ctx.Reservations.SingleOrDefault(r => (r.wishlistId == wishlistId));
        }

        //TODO als de film repository bestaat/werkt moet deze dus niet meer hier staan
        public IQueryable<Films> getFilms()
        {
            return ctx.Films;
        }
        public IQueryable<Specials> getSpecials()
        {
            return ctx.Specials;
        }

        public IQueryable<Restaurants> getRestaurants()
        {
            return ctx.Restaurants;
        }

        public Wishlists getWishList(string token)
        {
            return ctx.Wishlists.SingleOrDefault(w => (w.token == token));
        }
        //TODO als de film repository bestaat/werkt moet deze dus niet meer hier staan
        public IQueryable<Activities> getFilmActivities(int filmId)
        {
            return ctx.Filmscreenings.Where(s => s.filmId == filmId).Select(s => s.Activities);
        }

        public IQueryable<Activities> getSpecialActivities(int specialId)
        {
            return ctx.Specialscreenings.Where(s => s.specialId == specialId).Select(s => s.Activities);
        }
        

        public Activities GetWholeActivity(int activityId)
        {
            var activity = GetActivity(activityId)
                .Include(f => f.Filmscreenings.Select(a => a.Films))
                .Include(f => f.Filmscreenings.Select(r => r.Rooms.Locations))
                .Include(f => f.Specialscreenings.Select(a => a.Specials))
                .Include(f => f.Specialscreenings.Select(r => r.Rooms.Locations))
                .Include(r => r.Restaurants.Select(rr => rr.Locations));
            return activity.SingleOrDefault();
        }

        public IEnumerable<WishlistViewModel> GetActivities(Wishlists wishlist)
        {
            return GetActivities(wishlist.id);
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
            return list.OrderBy(order => order.TypeActivity);
            
        }
        public IEnumerable<WishlistViewModel> GetActivities(int wishlistId)
        {
            IList<WishlistViewModel> list = new List<WishlistViewModel>();
            //eagerloading to reduce queries to db to 1. without it lazyloading wil fire one by each loop
            var eagerloadingWishlistItems = GetWishlistItems(wishlistId)
                .Include(a => a.Activities)
                .Include(f => f.Activities.Filmscreenings.Select(a => a.Films))
                .Include(f => f.Activities.Filmscreenings.Select(r => r.Rooms.Locations))
                .Include(s => s.Activities.Specialscreenings.Select(a => a.Specials))
                .Include(s => s.Activities.Specialscreenings.Select(r => r.Rooms.Locations))
                .Include(r=>r.Activities.Restaurants.Select(rr=>rr.Locations));

            foreach (WishlistItems wishitem in eagerloadingWishlistItems)
            {
                list.Add(new WishlistViewModel(wishitem.Activities, wishitem.numberOfPersons));
            }
            return list.OrderBy(order => order.TypeActivity);
        }
        public Wishlists SaveActivities(List<WishlistViewModel> wishlist, Reservations reservation)
        {
            return SaveActivities(wishlist, reservation, null);
        }
        public Wishlists SaveActivities(List<WishlistViewModel> wishlist, Reservations reservation, Wishlists wishlistInSession)
        {
            Wishlists wislistToSave;
            //als de wishlist al een keer is opgehaald uit de db, update deze dan. 
            if (wishlistInSession != null)
            {
                wislistToSave = wishlistInSession;
                wislistToSave.WishlistItems = null;
                ctx.SaveChanges();
            }
            else
                wislistToSave = new Wishlists();

            wislistToSave.paid = false;
            foreach (WishlistViewModel activity in wishlist)
            {
                WishlistItems wishitems = new WishlistItems();
                //check of de activity daadwerkelijk in db staat
                Activities activiteit = activity.Activity;
                wishitems.activityId = activiteit.id;
                wishitems.numberOfPersons = activity.NumberOfPersons;
                wislistToSave.WishlistItems.Add(wishitems);
                //wislist.totalPrice = wislist.totalPrice + activiteit.price;
            }
            if (reservation != null)
            {
                wislistToSave.paid = true;
                wislistToSave.Reservations.Add(reservation);
            }
            wislistToSave.token = Crypto.HashPassword(Crypto.GenerateSalt() + DateTime.Now.Ticks.ToString());
            wislistToSave.totalPrice = wishlist.Sum(w => w.totalprice);
            if (wishlistInSession == null)
                ctx.Wishlists.Add(wislistToSave);
            ctx.SaveChanges();
            return wislistToSave;
        }

        public void ReservationOfActivities(List<WishlistViewModel> wishlist, Reservations reservation, Wishlists wishlistInSession)
        {
            SaveActivities(wishlist, reservation);
        }


    }
}
