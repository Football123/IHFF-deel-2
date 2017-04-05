using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IHffA7.Models;
using IHffA7.Models.repositories;

namespace IHffA7.Controllers
{
    //[Authorize]
    public class ActivitiesController : Controller
    {
        private ActivitiesRepository activitiesRepo = new ActivitiesRepository();
        private WhatsUp1617S_martinstinsGenerated db = new WhatsUp1617S_martinstinsGenerated();

        // GET: Activities
        public ActionResult Index()
        {
            return View(activitiesRepo.GetActivities());
        }

        public ActionResult CreateSpecial()
        {
            return View();
        }

        public ActionResult CreateRoom()
        {
            ViewBag.locationId = new SelectList(db.Locations, "id", "name");
            return View();
        }
        public ActionResult CreateFilm()
        {
            return View();
        }
        public ActionResult CreateLocation()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFilm(Films film)
        {
            if (ModelState.IsValid)
            {
                try {
                    activitiesRepo.SaveFilm(film);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return HttpNotFound();
                }
            }

            return View(film);
        }
        public ActionResult CreateRestaurant()
        {
            ViewBag.locationId = new SelectList(db.Locations, "id", "name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRestaurant(RestaurantsFormModel restaurant)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    activitiesRepo.SaveRestaurant(restaurant);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return HttpNotFound();
                }
            }
            //viewbaag moet weer opniew worden gemaakt
            ViewBag.locationId = new SelectList(db.Locations, "id", "name");

            return View(restaurant);
        }

        public ActionResult CreateFilmscreening()
        {
            FillFilmscreeninsDropdownsWithSecection(null,null);
            return View();
        }

        public ActionResult CreateSpecialsscreening()
        {
            FillSpecialsscreeninsDropdownsWithSecection(null,null);
            return View();
        }

        private void FillFilmscreeninsDropdownsWithSecection(int? roomId, int? filmId)
        {
            ViewBag.filmId = new SelectList(db.Films, "id", "title", filmId);
            ViewBag.roomId = new SelectList(db.Rooms, "id", "name", roomId);
        }

        private void FillSpecialsscreeninsDropdownsWithSecection(int? roomId, int? specialsId)
        {
            ViewBag.specialId = new SelectList(db.Specials, "id", "title", specialsId);
            ViewBag.roomId = new SelectList(db.Rooms, "id", "name", roomId);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFilmscreening(Filmscreenings screenings)
        {
            screenings.Activities.typeActivity = 1;
            screenings.activityId = screenings.Activities.id;
            if (ModelState.IsValid)
            {
                screenings.Activities.typeActivity = 1;
                screenings.activityId = screenings.Activities.id;
                try {
                    activitiesRepo.SaveFilmScreening(screenings);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return HttpNotFound();
                }
            }
            //viewbag moet weer opniew worden gevuld
            FillFilmscreeninsDropdownsWithSecection(screenings.roomId,screenings.filmId);
            return View(screenings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSpecialsscreening(Specialscreenings screening)
        {
            if (ModelState.IsValid)
            {
                screening.Activities.typeActivity = 2;
                screening.activityId = screening.Activities.id;
                try
                {
                    activitiesRepo.SaveSpecialsScreening(screening);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return HttpNotFound();
                }
            }
            //viewbag moet weer opniew worden gevuld
            FillSpecialsscreeninsDropdownsWithSecection(screening.roomId, screening.specialId);
            return View(screening);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSpecial(Specials special)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    activitiesRepo.SaveSpecial(special);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return HttpNotFound();
                }
            }
            return View(special);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateLocation(Locations location)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    activitiesRepo.SaveLocation(location);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return HttpNotFound();
                }
            }
            return View(location);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRoom(Rooms rooms)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    activitiesRepo.SaveRoom(rooms);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return HttpNotFound();
                }
            }
            return View(rooms);
        }

        public ActionResult EditFilmScreening(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {

                Filmscreenings filmscreening = activitiesRepo.GetFilmscreening(id);
                FillFilmscreeninsDropdownsWithSecection(filmscreening.roomId, filmscreening.filmId);
                ViewBag.IsEditing = true;
                return View("CreateFilmscreening", filmscreening);
            }
            catch
            {
                return  HttpNotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditFilmScreening(Filmscreenings screenings)
        {
            if (ModelState.IsValid)
            {
                screenings.Activities.typeActivity = 1;
                screenings.Activities.id = screenings.activityId;
                try {
                    activitiesRepo.ModifyActivity(screenings);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return HttpNotFound();
                }
                
            }
            ViewBag.IsEditing = true;
            FillFilmscreeninsDropdownsWithSecection(screenings.roomId, screenings.filmId);
            return View(screenings);
        }

        public ActionResult DeleteFilmScreening(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(activitiesRepo.GetFilmscreening(id));
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("DeleteFilmScreening")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            try
            {
                var filmscreening = activitiesRepo.GetFilmscreening(id);
                activitiesRepo.DeleteFilmScreening(filmscreening);
                return RedirectToAction("Index");
            }
            catch
            {
                return HttpNotFound();
            }
        }
    }
}
