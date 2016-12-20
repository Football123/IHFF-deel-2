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
    public class HomeController : Controller
    {
        WishlistRepository wishListRepository = new WishlistRepository();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            

            return View(wishListRepository.GetFilm(1));
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult OrderTicket(int eventId)
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ViewSchedule()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}