using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IHffA7.Models;
using System.Web.Security;
using IHffA7.Models.repositories;
using System.Collections;

namespace IHffA7.Controllers
{
    public class FilmController : Controller
    {
        private FilmRepository filmRepository= new FilmRepository();
        public ActionResult Index()
        {         
            IEnumerable<Films> film = filmRepository.GetFilms();
            return View(film);
        }
  
        private WhatsUp1617S_martinstinsGenerated ctx = new WhatsUp1617S_martinstinsGenerated();
        public ActionResult detailpage(int filmId)
        {        
            Films film = filmRepository.GetFilm(filmId);
            ViewBag.activityod = new SelectList(ctx.Activities, "id", "startTime");
            return View(film);
        }

        public ActionResult GetFilmMaandag()
        {           
            IEnumerable<Films> film = filmRepository.GetFilmMonday();
            return View("Index", film);
        }
        public ActionResult GetFilmDinsdag()
        {         
            IEnumerable<Films> film = filmRepository.GetFilmTuesday();
            return View("Index", film);
        }
        public ActionResult GetFilmWoensdag()
        {
            IEnumerable<Films> film = filmRepository.GetFilmWednesday();
            return View("Index", film);
        }
        public ActionResult GetFilmDonderdag()
        {            
            IEnumerable<Films> film = filmRepository.GetFilmThursday();
            return View("Index", film);
        }
        public ActionResult GetFilmVrijdag()
        {          
            IEnumerable<Films> film = filmRepository.GetFilmFriday();
            return View("Index", film);
        }
        //public ActionResult Detail(int filmId)
        //{       
        //    Films film = null; ; //filmRepository.GetFilm(filmId);
        //    return View(film);
        //}
        //public ActionResult AddToWishlist(int eventId, int typeId, int aantPersonen)
        //{
        //    return View();
        //}
    }
}