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

            return View("Index", GetActivitiesFromSession());
        }

        //wishlist bestaat nog niet, dus ook niet de items waar number of persons in staat
        public ActionResult AddItemToSesWishlist(int numberOfpersones, int activityId)
        {
            Activities activity = wishListRepo.GetWholeActivity(activityId);
            if (activity == null)
            {
                ViewBag.errors = "Kon activiteit niet vinden";
                return View("Index", GetActivitiesFromSession());
            }
            else
                AddActivityToSesWishlist(numberOfpersones, activity);
            return View("Index", GetActivitiesFromSession());
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

        private bool DeliteActivityFromSesWishlist(int activityId)
        {
            List<WishlistViewModel> wishlist = GetActivitiesFromSession();
            if (wishlist != null)
            {
                wishlist.RemoveAll(x => (x.Activity.id == activityId));
                Session["wishlistSessionList"] = wishlist;
                return true;
            }
            else
            {
                return false;
            }
        }

        public ActionResult RemoveActivityFromSesWishlist(int activityId)
        {
            if (DeliteActivityFromSesWishlist(activityId))
                ViewBag.succes = "Item Verwijderd";
            else
                ViewBag.errors = "Kon item niet verwijderen";
            return View("Index", GetActivitiesFromSession());
        }

        //ajaxGET from index
        //returns if valid a bootstap modal to edit wishlist item
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
                    //ViewBag.dropDown = new SelectList(wishListRepo.getFilms(), "id", "title", activiteit.Activity.Filmscreenings.First().Films.id);
                    int actionId;
                    string action;
                    int activityType = activiteit.Activity.typeActivity;
                    switch (activityType)
                    {
                        case 1:
                            actionId = activiteit.Activity.Filmscreenings.First().Films.id;
                            action = "Films";
                            ViewBag.ActionId = new SelectList(wishListRepo.getFilms(), "id", "title", actionId);
                            break;
                        case 2:
                            actionId = activiteit.Activity.Specialscreenings.First().Specials.id;
                            action = "Specials";
                            ViewBag.ActionId = new SelectList(wishListRepo.getSpecials(), "id", "title", actionId);
                            break;
                        case 3:
                            actionId = activiteit.Activity.Restaurants.First().id;
                            action = "Restaurants";
                            ViewBag.ActionId = new SelectList(wishListRepo.getRestaurants(), "id", "locationId", actionId);
                            break;
                        default:
                            ViewBag.errors = "Activiteitttype niet herkent";
                            return View("Index", GetActivitiesFromSession());
                    }
                    EditActivityViewModel viewModel = new EditActivityViewModel(activiteit.Activity.id, actionId, action, activiteit.NumberOfPersons);
                    return PartialView(viewModel);
                }
                catch
                {
                    ViewBag.errors = "Wishlistitem niet gevonden";
                }
            }
            ViewBag.errors = "Wishlistitem niet gevonden";
            return View("Index", GetActivitiesFromSession());
        }

        [HttpGet]
        public ActionResult GetScreeningsOrVisit(string action, int actionId)
        {
            IEnumerable<Activities> screenings = null;
            switch (action)
            {
                case "Films":
                    screenings = wishListRepo.getFilmActivities(actionId);
                    break;
                case "Specials":
                    screenings = wishListRepo.getSpecialActivities(actionId);
                    break;
                case "Restaurants":
                    break;
                default:
                    screenings = null;
                    break;
            }

            if (screenings != null)
            {
                ViewBag.NewActivityId = new SelectList(screenings, "id", "startTime", null);
            }
            
            return PartialView(new EditActivityViewModel());
        }

        [HttpPost]
        public ActionResult StoreEditActivityFromSesWishlist(EditActivityViewModel editedActivity)
        {
            ViewBag.succes = "Item succesvol gewijzig";
            //betekent dat er geen nieuwe activiteit is geselecteerd, maar aantalpersonen wel!
            //aantal personen wordt gewijzigd door item te verwijderen en opnieuw aan te maken. 
            if (editedActivity.NewActivityId == 0)
            {
                editedActivity.NewActivityId = editedActivity.OldActivityId;
            }
            DeliteActivityFromSesWishlist(editedActivity.OldActivityId);
            AddItemToSesWishlist(editedActivity.NumberOfPersons, editedActivity.NewActivityId);

            ViewBag.succes = "item gewijzigd";
            return View("Index", GetActivitiesFromSession());
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