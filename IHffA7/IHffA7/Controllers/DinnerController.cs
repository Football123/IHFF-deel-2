using IHffA7.Models;
using IHffA7.Models.repositories;
using System.Collections.Generic;
using System.Web.Mvc;

namespace IHffA7.Controllers
{
    public class DinnerController : Controller
    {
        private DinnerRepository dinnerRepository = new DinnerRepository();

        // GET: Dinner
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Restaurants()
        {
            IEnumerable<RestaurantOverviewModel> allRestaurants = dinnerRepository.GetAllRestaurants();

            return View(allRestaurants);
        }

        public ActionResult AddToWishlist(int eventId, int typeId, int aantPersonen, string date, string time)
        {
            return View();
        }
    }
}