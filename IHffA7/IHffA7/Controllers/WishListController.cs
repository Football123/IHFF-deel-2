using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IHffA7.Models;
using IHffA7.Models.repositories;
using System.Security.Cryptography;

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
            foreach (var item in activities)
            {
                AddActivityToSesWishlist(item.NumberOfPersons, item.Activity.id);
            }
            return RedirectToAction("Index", activities);
        }

        //wishlist bestaat nog niet, dus ook niet de items waar number of persons in staat
        public ActionResult AddItemToSesWishlist(int numberOfpersones, int activityId)
        {
            AddActivityToSesWishlist(numberOfpersones, activityId);
            return RedirectToAction("Index");
        }

        public void AddActivityToSesWishlist(int numberOfpersones, int activityId)
        {
            List<WishlistSession> wishlistSessionList = new List<WishlistSession>(); ;
            bool inList = false;
            if (Session["wishlistSessionList"] == null) //als niet bestaat, maak aan
            {
                Session["wishlistSessionList"] = wishlistSessionList;

            }
            else // anderes ophalen
            {
                wishlistSessionList = (List<WishlistSession>)Session["wishlistSessionList"];
                //check op dubbelen, zo ja tel personen op
                if (wishlistSessionList.Select(s => (s.ActivityId == activityId)).Count() >1)
                {

                    wishlistSessionList.First(s => (s.ActivityId == activityId)).NumberOfpersones += numberOfpersones;
                    inList = true;
                }
            }
            if (!inList)
            {
                wishlistSessionList.Add(new WishlistSession(activityId, numberOfpersones));
            }
            // check of hij niet bestaal in de lijst

            Session["filmlist"] = wishlistSessionList; // wijzigingen opslaan
        }

        public ActionResult RemoveActivityFromSesWishlist(int activityId)
        {
            List<WishlistSession> wishlistSessionList = new List<WishlistSession>();
            if (Session["wishlistSessionList"] != null)
            {
                wishlistSessionList = (List<WishlistSession>)Session["filmlist"];
                wishlistSessionList.RemoveAll(x => (x.ActivityId == activityId));
                Session["wishlistSessionList"] = wishlistSessionList;
            }
            return RedirectToAction("Index");
        }
        public IEnumerable<WishlistViewModel> GetActivitiesFromSession()
        {
            List<WishlistSession> wishlistSessionList = new List<WishlistSession>();
            IEnumerable<WishlistViewModel> activitiesList;
            if (Session["wishlistSessionList"] != null)
            {
                wishlistSessionList = (List<WishlistSession>)Session["wishlistSessionList"];
                activitiesList = wishListRepo.GetActivities(wishlistSessionList);
                return activitiesList;
            }
            return null;
        }


        public ActionResult SaveWishlist()
        {

            List<WishlistSession> wishlistSessionList = new List<WishlistSession>();;
            if (Session["wishlistSessionList"] != null)
            {
                wishlistSessionList = (List<WishlistSession>)Session["wishlistSessionList"];
                try {
                    wishListRepo.SaveActivities(wishlistSessionList);
                    ViewBag.SuccesMessage = "succesvol opgeslagen";
                }
                catch
                {
                    ViewBag.SuccesMessage = "Iets ging mis!";
                }
            }
            return View("Index", GetActivitiesFromSession());
        }
        public ActionResult PayWishlist()
        {
            List<WishlistSession> wishlistSessionList = new List<WishlistSession>(); ;
            if (Session["wishlistSessionList"] != null)
            {
                wishlistSessionList = (List<WishlistSession>)Session["wishlistSessionList"];
                try
                {
                    wishListRepo.ReservationOfActivities(wishlistSessionList);
                    ViewBag.SuccesMessage = "Betaling gelukt!";
                }
                catch
                {
                    ViewBag.SuccesMessage = "Iets ging mis!";
                }
            }
            return View("Index", GetActivitiesFromSession());
        }
    }
}