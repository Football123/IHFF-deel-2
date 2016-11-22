using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IHffA7.Models.dbModels;
using IHffA7.Models;

namespace IHffA7.Controllers
{
    public class HomeController : Controller
    {
        private IRepository repository = new Repository();
        public ActionResult Index()
        {
            IEnumerable<Testtabel> i= repository.GetallConten();

            Testtabel z = new Testtabel( "jan");
            repository.Addrij(z);

            return View(i);
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