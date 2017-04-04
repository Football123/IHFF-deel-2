using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IHffA7.Models;
using IHffA7.Models.repositories;

namespace IHffA7.Controllers
{
    public class WishListController : Controller
    {
        private WishlistRepository wishListRepo;

        public WishListController()
        {
            wishListRepo = new WishlistRepository();
        }

        public ActionResult Index()
        {
            IEnumerable<WishlistViewModel> activities = GetActivitiesFromSession();
            return View(activities);
        }

        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public ActionResult GetSavedWishlist(int? wislistId)
        {
            if (wislistId == null)
            {
                @ViewBag.errors = "Code niet geldig";
                return View("Index", GetActivitiesFromSession());
            }
            IEnumerable<WishlistViewModel> activities = wishListRepo.GetActivities((int)wislistId);
            if(activities.Count() <1)
            {
                @ViewBag.errors = "Code niet gevonden";
                return View("Index", GetActivitiesFromSession());
            }
            foreach (var activity in activities)
            {
                AddActivityToSesWishlist(activity.NumberOfPersons, activity.Activity);
            }
            ViewBag.succes = "Wishslist geladen";
            
            return RedirectToAction("Index", activities);
        }

        //wishlist bestaat nog niet, dus ook niet de items waar number of persons in staat
        public ActionResult AddItemToSesWishlist(int numberOfpersones, int activityId)
        {
            Activities activity = wishListRepo.GetWholeActivity(activityId);
            AddActivityToSesWishlist(numberOfpersones, activity);
            return RedirectToAction("Index");
        }

        private void AddActivityToSesWishlist(int numberOfpersones, Activities activity)
        {
            WishlistViewModel newitem = new WishlistViewModel(activity, numberOfpersones);
            List<WishlistViewModel> wishlist = new List<WishlistViewModel>();
            //aanmaken indien nog niet bestaat
            if(Session["wishlistSessionList"] == null)
            {
                Session["wishlistSessionList"] = wishlist;
            }
            var currentWishList = Session["wishlistSessionList"];
            bool inList = false;
            if (currentWishList == null)
            {
                currentWishList = wishlist;
            }
            else
            {
                wishlist = (List<WishlistViewModel>)currentWishList;
                try
                {
                    wishlist.FindAll(x => (x.Activity.id == activity.id))
                        .Single().NumberOfPersons += numberOfpersones;
                    inList = true;
                }
                catch
                {
                    inList = false;
                }
                if (!inList)
                {
                    wishlist.Add(new WishlistViewModel(activity, numberOfpersones));
                }
            }
            //wijzigingen opslaan
            Session["wishlistSessionList"] = wishlist;

        }

        public ActionResult RemoveActivityFromSesWishlist(int activityId)
        {
            List<WishlistViewModel> wishlist = GetActivitiesFromSession();
            if (wishlist != null)
            {
                wishlist.RemoveAll(x => (x.Activity.id == activityId));
                Session["wishlistSessionList"] = wishlist;
            }
            return RedirectToAction("Index");
        }

        //ajaxGET from index
        //returns if valid a modal
        [HttpGet]
        public ActionResult EditActivityFromSesWishlist(int activityId)
        {
            List<WishlistViewModel> wishlist = GetActivitiesFromSession();
            WishlistViewModel activiteit;
            if (wishlist != null)
            {
                try
                {
                    activiteit = wishlist.FindAll(x => (x.Activity.id == activityId))
                        .Single();
                    ViewBag.succes = "item gevonden";
                    ViewBag.filmsDropDown = new SelectList(wishListRepo.getFilms(), "id", "title", activiteit.Activity.Filmscreenings.First().Films.id);
                
                    return PartialView(activiteit);
                }
                catch
                {
                    ViewBag.errors = "Wishlistitem niet gevonden";
                }
            }
            ViewBag.errors = "geen item gevonden";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult GetFilmScreenings(int filmId)
        {
            IEnumerable<Activities> screenings = wishListRepo.getFilmActivities(filmId);
            var Starttijden = screenings.Select(a => a.startTime);
            ViewBag.filmScreeiningsDropDown = new SelectList(screenings, "id", "startTime", null);
            var debug = screenings.FirstOrDefault();
            return PartialView(screenings.FirstOrDefault());
        }

        [HttpPost]
        public ActionResult EditActivityFromSesWishlist(WishlistViewModel wishlistItem)
        {
            var debug = wishlistItem;
            ViewBag.succes = "Item succesvol gewijzig";
            return RedirectToAction("Index");
        }
        public ActionResult EditActivityFromSesWishlistOLDEREE(int activityId)
        {
            List<WishlistViewModel> wishlist = GetActivitiesFromSession();
            WishlistViewModel activiteit;
            if (wishlist != null)
            {
                try
                {
                    activiteit = wishlist.FindAll(x => (x.Activity.id == activityId))
                        .Single();
                    ViewBag.filmsDropDown = new SelectList(wishListRepo.getFilms(), "id", "title", activiteit.Activity.Filmscreenings.First().Films.id);
                    return PartialView(activiteit);
                }
                catch
                {
                    ViewBag.errors = "Wishlistitem niet gevonden";
                } 
            }
            return RedirectToAction("Index");
        }



        public List<WishlistViewModel> GetActivitiesFromSession()
        {
            List<WishlistViewModel> activitiesList;
            var currentWishList = Session["wishlistSessionList"];
            if (currentWishList != null)
            {
                activitiesList = (List<WishlistViewModel>)currentWishList;
                return activitiesList;
            }
            return null;
        }


        public ActionResult SaveWishlist()
        {

            List<WishlistViewModel> wishlist = GetActivitiesFromSession();
            if (wishlist != null)
            {
                try {
                    wishListRepo.SaveActivities(wishlist, null);
                    ViewBag.succes = "succesvol opgeslagen";
                }
                catch
                {
                    ViewBag.errors = "Opslagen mislukt";
                }
            }
            return View("Index", GetActivitiesFromSession());
        }
        public ActionResult PayWishlist()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PayWishlist(Reservations reservation)
        {
            if (ModelState.IsValid)
            {
                List<WishlistViewModel> wishlist = GetActivitiesFromSession();
                if (wishlist != null)
                {
                    wishListRepo.ReservationOfActivities(wishlist, reservation);
                    ViewBag.succes = "Betaling voltooid";
                    return View("Index", GetActivitiesFromSession());
                }
                    
            }
            ViewBag.errors = "Betaling mislukt!";
            return View();
        }
    }
}