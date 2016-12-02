using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IHffA7.Models.dbModels;
using IHffA7.Models;

namespace IHffA7.Controllers
{
    public class WishListController : Controller
    {
        // GET: WishList
        public ActionResult Index()
        {
            WishlistItemFilm film = new WishlistItemFilm(1);

            return View(film);
        }
    }
}