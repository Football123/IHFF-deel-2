using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IHffA7.Models;
using System.Web.Security;
using IHffA7.Models.dbModels;

namespace IHffA7.Controllers
{
    public class FilmController : Controller
    {
        private IFilmRepository filmrepository = new FilmRepository();

        // GET: Film
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddToWishlist(int eventId, int typeId, int aantPersonen)
        {
            return View();
        }
    }
}