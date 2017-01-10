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
            IEnumerable<SelectListItem> items = db.Filmscreenings.Select(c => new SelectListItem
            {
                Value = c.Films.id.ToString(),
                Text = c.Films.title.ToString()
            });
            ViewBag.FilmId = items;
            ViewBag.activityId = new SelectList(db.Activities, "id", "id");
            ViewBag.filmId = new SelectList(db.Films, "id", "title");
            ViewBag.roomId = new SelectList(db.Rooms, "id", "name");
            return View();
        }

        public ActionResult Create2()
        {
            IEnumerable<SelectListItem> items = db.Filmscreenings.Select(c => new SelectListItem
            {
                Value = c.Films.id.ToString(),
                Text = c.Films.title.ToString()
            });
            ViewBag.FilmId = items;
            ViewBag.activityId = new SelectList(db.Activities, "id", "id");
            ViewBag.filmId = new SelectList(db.Films, "id", "title");
            ViewBag.roomId = new SelectList(db.Rooms, "id", "name");
            return View();
        }
        //not nin use, but nice xample of and selectlit
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
        public ActionResult Create2( Filmscreenings screenings)
        {
            if (ModelState.IsValid)
            {
                screenings.Activities.typeActivity = 1;
                screenings.activityId = screenings.Activities.id;
                //test to so what is in it
                activitiesRepo.SaveFilmScreening(screenings);
                return RedirectToAction("Index");
            }
            //viewbaag moet weer opniew worden gemaakt
            ViewBag.activityId = new SelectList(db.Activities, "id", "id");
            ViewBag.filmId = new SelectList(db.Films, "id", "title");
            ViewBag.roomId = new SelectList(db.Rooms, "id", "name");

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
        public ActionResult Edit(int? id)
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

        // POST: Activities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,startTime,endTime,price,highlight,typeActivity")] Activities activities)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activities).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(activities);
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
