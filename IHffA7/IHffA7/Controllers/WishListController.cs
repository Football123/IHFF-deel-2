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
                @ViewBag.ValidationMessage = "Code bestaat niet.";
                return View("Index", GetActivitiesFromSession());
            }
            IEnumerable<WishlistViewModel> activities = wishListRepo.GetActivities((int)wislistId);
            foreach (var activity in activities)
            {
                AddActivityToSesWishlist(activity.NumberOfPersons, activity.Activity);
            }
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
        public ActionResult PayWishlist(Reservations reservation)
        {
            if (ModelState.IsValid)
            {
                List<WishlistViewModel> wishlist = GetActivitiesFromSession();
                if (wishlist != null)
                {
                    wishListRepo.ReservationOfActivities(wishlist, reservation);
                    ViewBag.SuccesMessage = "betaling voltooid";
                    return View("Index", GetActivitiesFromSession());
                }
                    
            }
            ViewBag.SuccesMessage = "Betaling mislukt!";
            return View();
        }
    }
}