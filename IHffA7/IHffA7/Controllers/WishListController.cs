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
            return RedirectToAction("GetSavedWishlist");
        }

        public ActionResult GetSavedWishlist()
        {
            int wislistId = 1;
            Wishlist wishlist = new Wishlist(wishListRepo.GetAllFilmActivies(wislistId));
            return View(wishlist);
        }

        //alle films staan al in de database
        public ActionResult AddFilmToSesWishlist(int numberOfpersones, int activityId, DateTime start, DateTime end)
        {
            AddAFilmToSesWishlist(numberOfpersones, activityId, start, end);
            return RedirectToAction("Index", "WishList");
        }
//methode
        public ActionResult RemoveFilmFromSesWishlist(int numberOfpersones, int activityId, DateTime start, DateTime end)
        {
            SessionFilm film = new SessionFilm(numberOfpersones, activityId);
            List<SessionFilm> filmlist = new List<SessionFilm>();
            if (Session["filmlist"] != null)
            {
                filmlist = (List<SessionFilm>)Session["filmlist"];
                filmlist.ToList();
                foreach (var i in filmlist)
                {
                    if (film ==i)
                    {
                        filmlist.RemoveAt(1);
                        filmlist = (List<SessionFilm>)Session["filmlist"];
                        break;
                    }
                }
            }
            else
            { // untested what it actually does
                ModelState.AddModelError("invalidItem", "invalid item");
            }
            return RedirectToAction("Index");
        }

//AddAFilmToSesWishlist
        public void AddAFilmToSesWishlist(int numberOfpersones, int activityId, DateTime start, DateTime end)
        {
            SessionFilm film = new SessionFilm(numberOfpersones, activityId);
            List<SessionFilm> filmlist = new List<SessionFilm>();
            if (Session["filmlist"] == null) //als niet bestaat, maak aan
            {
                Session["filmlist"] = filmlist;
            }
            else // anderes ophalen
            {
                filmlist = (List<SessionFilm>)Session["filmlist"];
            }
            // check of hij niet bestaal in de lijst

            Session["filmlist"] = filmlist; // wijzigingen opslaan
        }

 //methode
        public IList<WishlistItemFilm> GetFilmsFromSession()
        {
            List<SessionFilm> filmlist = new List<SessionFilm>();
            IList<WishlistItemFilm> wishlistItemsFilmList;
            if (Session["filmlist"] != null)
            {
                filmlist = (List<SessionFilm>)Session["filmlist"];
                wishlistItemsFilmList = null; // wishListRepository.GetAllWishlistFilms(filmlist);
            }
            else // als niet bestaat maakt null, view model support geen nullables 
            {
                wishlistItemsFilmList = null;
            }
            return wishlistItemsFilmList;
        }

        public ActionResult SaveWishlist(string email)
        {
            //save the wish list and get de wishlist id code, maak hier de code van
            // get saved wishlist kan dan niet meer, omdat de id er niet uit te halen is...
            string code = CodeGenerator.Encrypt(1.ToString(), email);
            return View();
        }
    }
}