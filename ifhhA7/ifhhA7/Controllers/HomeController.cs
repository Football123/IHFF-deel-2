﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ifhhA7.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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

        public ActionResult Films()
        {
            return View();
        }

        public ActionResult Restaurants()
        {
            return View();
        }

        public ActionResult IHff()
        {
            return View();
        }

        public ActionResult OrderTicket()
        {
            return View();
        }

        public ActionResult ViewSchedule()
        {
            return View();
        }
    }
}