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
        WishlistRepository wishListRepository = new WishlistRepository();
        // GET: WishList
        public ActionResult Index()
        {
            IEnumerable<Locations> test2 = wishListRepository.GetLocations();

            IEnumerable<Models.dbModels.WishlistItems> test = wishListRepository.GetWishlistItems(1);
            Activities activiteit = wishListRepository.GetActiviteit(1);

            FilmScreenings filmvoorstelling = wishListRepository.GetFilmvoorstelling(1);
            Films film = wishListRepository.GetFilm(1);
            WishlistItemFilm filmsInWishlist = new Models.WishlistItemFilm(1);
            return View(filmsInWishlist);
        }
    }
}