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

        public ActionResult GetSavedWishlist(int wislistId)
        {
            //int wislistId = 1;
            IEnumerable<WishlistViewModel> activities = wishListRepo.GetActivities(wislistId);
            foreach (var item in activities)
            {
                AddActivityToSesWishlist(item.NumberOfPersons, item.Activity.id);
            }
            return RedirectToAction("Index", activities);
        }

        //niet verwijderen, is functioneel!
        public ActionResult AddItemToSesWishlist(int numberOfpersones, int activityId)
        {
            AddActivityToSesWishlist(numberOfpersones, activityId);
            return RedirectToAction("Index");
        }
        //methode
        public ActionResult RemoveFilmFromSesWishlist( int activityId)
        {
            List<SessionFilm> filmlist = new List<SessionFilm>();
            if (Session["filmlist"] != null)
            {
                filmlist = (List<SessionFilm>)Session["filmlist"];
                //var filmpkje= filmlist.Select(f => (f.ActivityId==activityId)).SingleOrDefault();
                //filmlist.RemoveAt(1);
                filmlist.RemoveAll(x => (x.ActivityId == activityId));
                Session["filmlist"] = filmlist;

                /*
                filmlist.ToList();
                foreach (var i in filmlist)
                {
                    if (film ==i)
                    {
                        filmlist.Remove(i);
                        filmlist = (List<SessionFilm>)Session["filmlist"];
                        break;
                    }
                }*/
            }
            else
            { // untested what it actually does
                ModelState.AddModelError("invalidItem", "invalid item");
            }
            return RedirectToAction("Index");
        }

//AddAFilmToSesWishlist
        public void AddAFilmToSesWishlist(int numberOfpersones, int activityId)
        {
            SessionFilm film = new SessionFilm(numberOfpersones, activityId, null);
            List<SessionFilm> filmlist = new List<SessionFilm>();
            bool inList = false;
            if (Session["filmlist"] == null) //als niet bestaat, maak aan
            {
                Session["filmlist"] = filmlist;
                
            }
            else // anderes ophalen
            {
                filmlist = (List<SessionFilm>)Session["filmlist"];
                //check op dubbelen
                foreach(SessionFilm item in filmlist)
                {
                    if (item.ActivityId== activityId)
                    {
                        item.NumberOfpersones = +numberOfpersones;
                        inList = true;
                        break;
                    }
                }
            }
            if (!inList)
            {
                filmlist.Add(film);
            }
            // check of hij niet bestaal in de lijst

            Session["filmlist"] = filmlist; // wijzigingen opslaan
        }

 //methode
        public IEnumerable<WishlistItemFilm> GetFilmsFromSession()
        {
            List<SessionFilm> filmlist = new List<SessionFilm>();
            IEnumerable<WishlistItemFilm> wishlistItemsFilmList;
            if (Session["filmlist"] != null)
            {
                filmlist = (List<SessionFilm>)Session["filmlist"];
                wishlistItemsFilmList = wishListRepo.getFilmactivities(filmlist); // wishListRepository.GetAllWishlistFilms(filmlist);
            }
            else // als niet bestaat maakt null, view model support geen nullables 
            {
                wishlistItemsFilmList = null;
            }
            return wishlistItemsFilmList;
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
                //check op dubbelen
                foreach (WishlistSession item in wishlistSessionList)
                {
                    if (item.ActivityId == activityId)
                    {
                        item.NumberOfpersones = +numberOfpersones;
                        inList = true;
                        break;
                    }
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
                activitiesList = wishListRepo.getActivities(wishlistSessionList);
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
    }
}