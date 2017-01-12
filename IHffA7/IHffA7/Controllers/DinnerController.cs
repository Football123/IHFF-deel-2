using IHffA7.Models;
using IHffA7.Models.repositories;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace IHffA7.Controllers
{
    public class DinnerController : Controller
    {
        private DinnerRepository dinnerRepository = new DinnerRepository();

        // GET: Dinner
        public ActionResult Restaurants()
        {
            IEnumerable<RestaurantOverviewModel> allRestaurants = dinnerRepository.GetAllRestaurants();

            return View(allRestaurants);
        }

        [HttpPost]
        public ActionResult Restaurants(RestaurantOverviewModel rom) //Dropdownlist items: Datum, Tijd, AantalPersonen -- Restaurant: Id
        {
            DateTime datum = rom.Datum;
            TimeSpan tijd = rom.Tijd;

            DateTime datumTijd = datum + tijd;
            rom.Datum = datumTijd;

            return RedirectToAction("AddItemToSesWishlist", "WishList", rom);
        }
    }
}