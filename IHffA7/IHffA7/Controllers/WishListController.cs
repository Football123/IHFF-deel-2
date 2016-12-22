using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IHffA7.Models.dbModels;
using IHffA7.Models;
using IHffA7.Models.repositories;
using System.Security.Cryptography;

namespace IHffA7.Controllers
{
    public class WishListController : Controller
    {
        private WishlistRepository wishListRepository = new WishlistRepository();
        public ActionResult Index()
        {
            List<SessionFilm> filmlist = new List<SessionFilm>();
//Get filmsFromSession            
            IList<WishlistItemFilm> wishlistItemsFilmList;
            if (Session["filmlist"] != null)
            {
                filmlist = (List<SessionFilm>)Session["filmlist"];
                wishlistItemsFilmList = wishListRepository.GetAllWishlistFilms(filmlist);
            }
            else // als niet bestaat maakt null, view model support geen nullables 
            {
                wishlistItemsFilmList = null;
            }
//Get restaurantsFromSession
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
            //return View(wishlistItemsFilmList); // oude alleen films vie ging deze heen
            return View(model);
        }

        public ActionResult GetSavedWishlist(int wislistId)
        { //GetWishlistFromDB
            IList<WishlistItemFilm> wishlistItemsFilmList = wishListRepository.GetAllWishlistFilms(wislistId);
            IList<WishlistItemRestaurant> wishlistItemsRestaurantList = wishListRepository.GetAllWishlistRestaurants(wislistId);
            WishlistViewModel model = new WishlistViewModel(wishlistItemsFilmList, wishlistItemsRestaurantList);
            //put this in the session to make this one editable.
            foreach(WishlistItemFilm film in wishlistItemsFilmList)
            {
                AddAFilmToSesWishlist(film.WishlistItem.NumberOfPersons, film.Activiteit.Id, film.Activiteit.StartTime, film.Activiteit.EndTime);
            }
            foreach(WishlistItemRestaurant restaurant in wishlistItemsRestaurantList)
            {
                AddARestaurantSesWishlist(restaurant.WishlistItem.NumberOfPersons, restaurant.Activiteit.StartTime, restaurant.Location.Id, restaurant.Restaurant.Id);
            }
            //store the wislistId in the session, so it can be edited and save changes. USE te code generator!
            if(wishlistItemsFilmList.ElementAtOrDefault(0)!= null)
            {
                Session["wishlistcode"] = wishlistItemsFilmList.ElementAt(0).WishlistItem.WishlistId;
            }
            if (wishlistItemsRestaurantList.ElementAtOrDefault(0)!=null)
            {
                Session["wishlistcode"] = wishlistItemsRestaurantList.ElementAt(0).WishlistItem.WishlistId;
            }
            return RedirectToAction("Index");
            //return View("Index", model);
        }

        //alle films staan al in de database
        public ActionResult AddFilmToSesWishlist(int numberOfpersones, int activityId, DateTime start, DateTime end)
        {
            AddAFilmToSesWishlist(numberOfpersones, activityId, start, end);
            return RedirectToAction("Index", "WishList");
        }

        public ActionResult RemoveFilmFromSesWishlist(int numberOfpersones, int activityId, DateTime start, DateTime end)
        {
            SessionFilm film = new SessionFilm(numberOfpersones, activityId, start, end);
            List<SessionFilm> filmlist = new List<SessionFilm>();
            if (Session["filmlist"] != null)
            {
                filmlist = (List<SessionFilm>)Session["filmlist"];
                foreach (var i in filmlist)
                {
                    if (i == film)
                    {
                        filmlist.Remove(i);
                        filmlist = (List<SessionFilm>)Session["filmlist"];
                        break;
                    }
                }
            }
            else
            {
                ModelState.AddModelError("invalidItem", "invalid item");
            }
            return RedirectToAction("Index");
        }

//AddAFilmToSesWishlist
        public void AddAFilmToSesWishlist(int numberOfpersones, int activityId, DateTime start, DateTime end)
        {
            SessionFilm film = new SessionFilm(numberOfpersones, activityId, start, end);
            List<SessionFilm> filmlist = new List<SessionFilm>();
            if (Session["filmlist"] == null) //als niet bestaat, maak aan
            {
                Session["filmlist"] = filmlist;
            }
            else // anderes ophalen
            {
                filmlist = (List<SessionFilm>)Session["filmlist"];
            }
            filmlist.Add(film);
            Session["filmlist"] = filmlist; // wijzigingen opslaan
        }
        //Is een probleem, omdat een restaurant uniek aan een activiteit is gekoppeld. Specials kent het zelfde probleem. je hebt echt een restautantId nodig, de activity en dus de PK activity id bestaat dus ook niet
        public ActionResult AddRestaurantSesWishlist(int numberOfpersones, DateTime startTime, int locationId, int restaurantId)
        {
            AddARestaurantSesWishlist(numberOfpersones, startTime, locationId, restaurantId);
            return RedirectToAction("Index", "WishList");
        }

//AddARestaurantSesWishlist
        public void AddARestaurantSesWishlist(int numberOfpersones, DateTime startTime, int locationId, int restaurantId)
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
            Session["restaurantlist"] = restaurantlist;
        }

        public ActionResult SaveWishlist()
        {
            //save the wish list and get de wishlist id code, maak hier de code van
            // get saved wishlist kan dan niet meer, omdat de id er niet uit te halen is...
            //CodeGenerator.CreateCode("2");
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