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
        // GET: WishList
        public ActionResult Index()
        {
            IList<WishlistItemFilm> wishlistItemsFilmList = wishListRepository.GetAllWishlistFilms(1);
            return View(wishlistItemsFilmList);
        }
    }
}