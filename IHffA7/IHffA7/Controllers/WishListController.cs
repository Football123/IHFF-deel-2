using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IHffA7.Models.dbModels;
using IHffA7.Models;
using IHffA7.Models.repositories;

namespace IHffA7.Controllers
{
    public class WishListController : Controller
    {
        private WishlistRepository wishListRepository = new WishlistRepository();
        public ActionResult Index()
        {
            List<SessionFilm> filmlist = new List<SessionFilm>();
            IList<WishlistItemFilm> wishlistItemsFilmList;
            if (Session["filmlist"] != null)
            {
                filmlist = (List<SessionFilm>)Session["filmlist"];
                wishlistItemsFilmList = wishListRepository.GetAllWishlistFilms(filmlist);
            }
            else
            {
                wishlistItemsFilmList = null;
            }

            List<SessionRestaurant> restaurantlist = new List<SessionRestaurant>();
            IList<WishlistItemRestaurant> wishlistItemsRestaurantList;
            if (Session["restaurantlist"] != null)
            {
                Session["restaurantlist"] = restaurantlist;
                wishlistItemsRestaurantList = wishListRepository.GetAllWishlistRestaurants(restaurantlist);
            }
            else
            {
                wishlistItemsRestaurantList = null;
            }

            WishlistViewModel model = new WishlistViewModel(wishlistItemsFilmList, wishlistItemsRestaurantList);
            //return View(wishlistItemsFilmList);
            return View(model);
        }
        public ActionResult GoToIndexView(IList<WishlistItemFilm> wishlistItemsFilmList)
        {
            return View(wishlistItemsFilmList);
        }

        public ActionResult GetSavedWishlist(int wislistId)
        { //GetWishlistFromDB(int wislistID) zou de echte naam moeten zijn en moettttt 
            IList<WishlistItemFilm> wishlistItemsFilmList = wishListRepository.GetAllWishlistFilms(wislistId);
  //to do de opgeslagen eenheden in de session list opslaan.
            return RedirectToAction("GoToIndexView", "Wishlist", wishlistItemsFilmList);
        }

        //alle films staan al in de database
        public ActionResult AddFilmToSesWishlist(int numberOfpersones, int activityId, DateTime start, DateTime end)
        {
            SessionFilm film = new SessionFilm(numberOfpersones, activityId, start, end);
            List<SessionFilm> filmlist = new List<SessionFilm>();
            if (Session["filmlist"] == null)
            {
                Session["filmlist"] = filmlist;
            }
            else
            {
                filmlist = (List<SessionFilm>)Session["filmlist"];
            }
            filmlist.Add(film);
            filmlist = (List<SessionFilm>)Session["filmlist"];
            return RedirectToAction("Index", "WishList");
        }

        public ActionResult AddRestaurantSesWishlist(int numberOfpersones, DateTime startTime, int locationId, int restaurantId)
        {
            SessionRestaurant restaurant = new SessionRestaurant(numberOfpersones, startTime, locationId, restaurantId);
            List<SessionRestaurant> restaurantlist = new List<SessionRestaurant>();
            if (Session["restaurantlist"] == null)
            {
                Session["restaurantlist"] = restaurantlist;
            }
            else
            {
                restaurantlist = (List<SessionRestaurant>)Session["restaurantlist"];
            }
            restaurantlist.Add(restaurant);
            restaurantlist = (List<SessionRestaurant>)Session["restaurantlist"];
            return RedirectToAction("Index", "WishList");
        }

        public ActionResult SaveWishlist()
        {
            //save the wish list and get de wishlist id code, maak hier de code van
            // get saveed wishlist kan dan niet meer, omdat de id er niet uit te halen is...
            CodeGenerator.CreateCode("2");
            return View();
        }


        /// <summary>
        /// TO DO specials in de sessie opslaan
        /// </summary>
        /// <returns></returns>
        public ActionResult Test()
        {
            SessionFilm film = new SessionFilm(101, 1, new DateTime(2016, 5, 4), new DateTime(2016, 5, 4));
            SessionFilm film2 = new SessionFilm(101, 1, new DateTime(2016, 5, 4), new DateTime(2016, 5, 4));
            List<SessionFilm> filmlist = new List<SessionFilm>();
            filmlist.Add(film);
            filmlist.Add(film2);

            //save list in session
            Session["filmlist"] = filmlist;

            List<SessionFilm> movies = new List<SessionFilm>();
            //retrieve it from session
            movies = (List<SessionFilm>)Session["filmlist"];
            return View(movies);
            //return RedirectToAction("Index", "Contact");
        }
    }
}