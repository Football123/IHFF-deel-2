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
    public class ActivitiesController : Controller
    {
        private ActivitiesRepository activitiesRepo = new ActivitiesRepository();
        private WhatsUp1617S_martinstinsGenerated db = new WhatsUp1617S_martinstinsGenerated();

        // GET: Activities
        public ActionResult Index()
        {
            return View(db.Activities.ToList());
        }

        // GET: Activities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activities activities = db.Activities.Find(id);
            if (activities == null)
            {
                return HttpNotFound();
            }
            return View(activities);
        }

        // GET: Activities/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult CreateFilm()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFilm(Films film)
        {
            if (ModelState.IsValid)
            {
                //test to so what is in it
                activitiesRepo.SaveFilm(film);
                return RedirectToAction("Index");
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
        public ActionResult CreateRestaurant(Restaurants restaurant)
        {
            if (ModelState.IsValid)
            {
                //test to so what is in it
                activitiesRepo.SaveRestaurant(restaurant);
                return RedirectToAction("Index");
            }
            //viewbaag moet weer opniew worden gemaakt
            ViewBag.locationId = new SelectList(db.Locations, "id", "name"); ;

            return View(restaurant);
        }

        public ActionResult CreateFilmscreening()
        {
            FillFilmscreeninsDropdowns();
            return View();
        }

        private void FillFilmscreeninsDropdowns()
        {
            ViewBag.activityId = new SelectList(db.Activities, "id", "id");
            ViewBag.filmId = new SelectList(db.Films, "id", "title");
            ViewBag.roomId = new SelectList(db.Rooms, "id", "name");
        }
        //not in use, but nice xample of and selectlit
        public ActionResult Create3()
        {
            IEnumerable<SelectListItem> items = db.Filmscreenings.Select(c => new SelectListItem
            {
                Value = c.Films.id.ToString(),
                Text = c.Films.title.ToString()
            });
            ViewBag.FilmId = items;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFilmscreening( Filmscreenings screenings)
        {
            if (ModelState.IsValid)
            {
                screenings.Activities.typeActivity = 1;
                screenings.activityId = screenings.Activities.id;
                //test to so what is in it
                activitiesRepo.SaveFilmScreening(screenings);
                return RedirectToAction("Index");
            }
            //viewbag moet weer opniew worden gevuld
            FillFilmscreeninsDropdowns();

            return View(screenings);
        }

        // POST: Activities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,startTime,endTime,price,highlight,typeActivity")] Activities activities)
        {
            if (ModelState.IsValid)
            {
                db.Activities.Add(activities);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(activities);
        }

        // GET: Activities/Edit/5
        public ActionResult EditOldAutoGen(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activities activities = db.Activities.Find(id);
            if (activities == null)
            {
                return HttpNotFound();
            }
            return View(activities);
        }
        public ActionResult EditFilmScreening(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activities activities = db.Activities.Find(id);
            if (activities == null)
            {
                return HttpNotFound();
            }
            //viewbaag moet weer opniew worden gemaakt
            FillFilmscreeninsDropdowns();
            return View("CreateFilmscreening", activities.Filmscreenings.Single());
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditFilmScreening(Filmscreenings screenings)
        {
            if (ModelState.IsValid)
            {
                screenings.Activities.typeActivity = 1;
                screenings.Activities.id = screenings.activityId;
                activitiesRepo.ModifyActivity(screenings);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSpecial(Specialscreenings screening)
        {
            if (ModelState.IsValid)
            {
                screening.Activities.typeActivity = 1;
                screening.Activities.id = screening.activityId;
                activitiesRepo.ModifyActivity(screening);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        // GET: Activities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activities activities = db.Activities.Find(id);
            if (activities == null)
            {
                return HttpNotFound();
            }
            return View(activities);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Activities activities = db.Activities.Find(id);
            db.Activities.Remove(activities);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
