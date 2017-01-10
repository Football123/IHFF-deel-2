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
            IEnumerable<WishlistItemFilm> films = GetFilmsFromSession();
            Wishlist wishlist = new Wishlist(films);
            ViewBag.TotalStudents = "hoi";
            return View("GetSavedWishlist", wishlist);
        }

        public ActionResult GetSavedWishlist(int wislistId)
        {
            //int wislistId = 1;
            Wishlist wishlist = new Wishlist(wishListRepo.getFilmactivities(wislistId));
            foreach(var item in wishlist.Films)
            {
                AddAFilmToSesWishlist(item.NumberOfPerons, item.ActivityId);
            }
            return RedirectToAction("Index");
            //return View(wishlist);
        }

        //alle films staan al in de database
        public ActionResult AddItemToSesWishlist(int numberOfpersones, int activityId, DateTime? startTime)
        {
            AddAFilmToSesWishlist(numberOfpersones, activityId);
            return RedirectToAction("Index", "WishList");
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

        public ActionResult SaveWishlist()
        {

            List<SessionFilm> filmlist = new List<SessionFilm>();;
            if (Session["filmlist"] != null)
            {
                filmlist = (List<SessionFilm>)Session["filmlist"];
                try {
                    wishListRepo.SaveFilmactivities(filmlist);
                    ViewBag.TotalStudents = "succesvol opgeslagen";
                }
                catch
                {
                    ViewBag.TotalStudents = "Iets ging mis!";
                }
            }
            return RedirectToAction("Index");
        }
    }
}