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
    {//hoi
        private FilmRepository filmRepository= new FilmRepository();
     

        //private IhffA7Context db = new IhffA7Context();
        // GET: Film
        public ActionResult Index()
        {
            //Hier komen alle films
            //var films = (from film in db.Film select film);
            //ViewBag.Film = films.ToList();
            Films film = filmRepository.GetFilm(1);
            return View(film);
        }

        public ActionResult Detail(int filmId)
        {
            /*var detail = (from film in db.Film select film);
            ViewBag.Details = detail.ToList();*/
            Films film = null; ; //filmRepository.GetFilm(filmId);
            return View(film);
        }
        public ActionResult AddToWishlist(int eventId, int typeId, int aantPersonen)
        {
            return View();
        }
    }
}