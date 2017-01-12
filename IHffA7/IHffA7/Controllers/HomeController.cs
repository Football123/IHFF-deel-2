using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IHffA7.Models;
using IHffA7.Models.repositories;

namespace IHffA7.Controllers
{
    public class HomeController : Controller
    {
        HomeRepository homeRepository = new HomeRepository();

        public ActionResult Index()
        {
            IEnumerable<FilmsTodayModel> filmTodayItems = homeRepository.GetTodaysMovies();

            return View(filmTodayItems);
        }

        [HttpPost]
        public ActionResult Index(FilmsTodayModel ftm)
        {
            int filmId = homeRepository.ConvertToFilmId(ftm);
             
            return RedirectToAction("Film", "Detail", filmId);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            

            return View();
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