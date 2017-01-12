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
            //Hier komen alle films    
            IEnumerable<Films> film = filmRepository.GetFilms();
            return View(film);
        }
        //verwijder en bren alles naar repo
        private WhatsUp1617S_martinstinsGenerated ctx = new WhatsUp1617S_martinstinsGenerated();
        public ActionResult detailpage(int filmId)
        {
            //Hier komen alle films
            Films film = filmRepository.GetFilm(filmId);
            ViewBag.activityod = new SelectList(ctx.Activities, "id", "startTime");
            return View(film);
        }

        //[HttpPost]
        //public ActionResult detailpage(int filmId)
        //{

        //    return View;                      
        //}

        public ActionResult GetFilmMaandag()
        {
            //Hier komen alle films
            IEnumerable<Films> film = filmRepository.GetFilmMonday();
            return View("Index", film);
        }
        public ActionResult GetFilmDinsdag()
        {
            //Hier komen alle films
            IEnumerable<Films> film = filmRepository.GetFilmTuesday();
            return View("Index", film);
        }
        public ActionResult GetFilmWoensdag()
        {
            //Hier komen alle films

            IEnumerable<Films> film = filmRepository.GetFilmWednesday();
            return View("Index", film);
        }
        public ActionResult GetFilmDonderdag()
        {
            //Hier komen alle films
            IEnumerable<Films> film = filmRepository.GetFilmThursday();
            return View("Index", film);
        }
        public ActionResult GetFilmVrijdag()
        {
            //Hier komen alle films
            IEnumerable<Films> film = filmRepository.GetFilmFriday();
            return View("Index", film);
        }
        public ActionResult Detail(int filmId)
        {       
            Films film = null; ; //filmRepository.GetFilm(filmId);
            return View(film);
        }
        public ActionResult AddToWishlist(int eventId, int typeId, int aantPersonen)
        {
            return View();
        }
    }
}