using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IHffA7.Models;

namespace IHffA7.Controllers
{
    public class FilmscreeningsController : Controller
    {
        private WhatsUp1617S_martinstinsGenerated db = new WhatsUp1617S_martinstinsGenerated();

        // GET: Filmscreenings
        public ActionResult Index()
        {
            var filmscreenings = db.Filmscreenings.Include(f => f.Activities).Include(f => f.Films).Include(f => f.Rooms);
            return View(filmscreenings.ToList());
        }

        // GET: Filmscreenings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Filmscreenings filmscreenings = db.Filmscreenings.Find(id);
            if (filmscreenings == null)
            {
                return HttpNotFound();
            }
            return View(filmscreenings);
        }

        // GET: Filmscreenings/Create
        public ActionResult Create()
        {
            ViewBag.activityId = new SelectList(db.Activities, "id", "id");
            ViewBag.filmId = new SelectList(db.Films, "id", "title");
            ViewBag.roomId = new SelectList(db.Rooms, "id", "name");
            return View();
        }

        // POST: Filmscreenings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "activityId,filmId,availableSeats,roomId")] Filmscreenings filmscreenings)
        {
            if (ModelState.IsValid)
            {
                db.Filmscreenings.Add(filmscreenings);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.activityId = new SelectList(db.Activities, "id", "id", filmscreenings.activityId);
            ViewBag.filmId = new SelectList(db.Films, "id", "title", filmscreenings.filmId);
            ViewBag.roomId = new SelectList(db.Rooms, "id", "name", filmscreenings.roomId);
            return View(filmscreenings);
        }

        // GET: Filmscreenings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Filmscreenings filmscreenings = db.Filmscreenings.Find(id);
            if (filmscreenings == null)
            {
                return HttpNotFound();
            }
            ViewBag.activityId = new SelectList(db.Activities, "id", "id", filmscreenings.activityId);
            ViewBag.filmId = new SelectList(db.Films, "id", "title", filmscreenings.filmId);
            ViewBag.roomId = new SelectList(db.Rooms, "id", "name", filmscreenings.roomId);
            return View(filmscreenings);
        }

        // POST: Filmscreenings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "activityId,filmId,availableSeats,roomId")] Filmscreenings filmscreenings)
        {
            if (ModelState.IsValid)
            {
                db.Entry(filmscreenings).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.activityId = new SelectList(db.Activities, "id", "id", filmscreenings.activityId);
            ViewBag.filmId = new SelectList(db.Films, "id", "title", filmscreenings.filmId);
            ViewBag.roomId = new SelectList(db.Rooms, "id", "name", filmscreenings.roomId);
            return View(filmscreenings);
        }

        // GET: Filmscreenings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Filmscreenings filmscreenings = db.Filmscreenings.Find(id);
            if (filmscreenings == null)
            {
                return HttpNotFound();
            }
            return View(filmscreenings);
        }

        // POST: Filmscreenings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Filmscreenings filmscreenings = db.Filmscreenings.Find(id);
            db.Filmscreenings.Remove(filmscreenings);
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
