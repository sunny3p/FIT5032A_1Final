using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032A_1Final.Models;
using Microsoft.AspNet.Identity;

namespace FIT5032A_1Final.Controllers
{
    public class Health_InfoController : Controller
    {
        private FIT5032_A1Final db = new FIT5032_A1Final();
        [Authorize]
        // GET: Health_Info
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("EmpDetail", "Health_Info");
            }
            else
            {
                var Id = User.Identity.GetUserId();
                var health_Info = db.Health_Info.Where(h => h.PId == Id);
                ViewBag.PId = Id;
                return View(health_Info.ToList());
            }
        }


        [Authorize]
        // GET: Health_Info/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Health_Info health_Info = db.Health_Info.Find(id);
            if (health_Info == null)
            {
                return HttpNotFound();
            }
            return View(health_Info);
        }

        [Authorize]
        // GET: Health_Info/Create
        public ActionResult Create()
        {
            ViewBag.PId = new SelectList(db.Personal_Info, "Id", "Fname");
            return View();
        }

        [Authorize]
        public ActionResult Calendar()
        {
            return View();
        }

        public JsonResult getReservationDetails()
        {
            var id = User.Identity.GetUserId();
            var events = db.Reservations.Where(x => x.EId == id)
                .Select(x => new { x.R_Id, x.Personal_Info.Fname, x.R_Status, x.R_DateTime }).ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        // GET: Healths
        [Authorize(Roles = "Admin")]
        public ActionResult EmpDetail()
        {
            var Id = User.Identity.GetUserId();
            var detail = db.Reservations.Where(h => h.EId == Id).ToList();
            ViewBag.EId = Id;
            return View(detail.ToList());
        }

        // POST: Health_Info/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Alchol_Consumption,Smoking,Height,Weight,Mood_Level,Date")] Health_Info health_Info)
        {
            health_Info.PId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Health_Info.Add(health_Info);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PId = new SelectList(db.Personal_Info, "Id", "Fname", health_Info.PId);
            return View(health_Info);
        }

        [Authorize]
        // GET: Health_Info/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Health_Info health_Info = db.Health_Info.Find(id);
            if (health_Info == null)
            {
                return HttpNotFound();
            }

            ViewBag.Date = health_Info.Date.ToShortDateString();
            ViewBag.PId = new SelectList(db.Personal_Info, "Id", "Fname", health_Info.PId);
            return View(health_Info);
        }

        // POST: Health_Info/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Alchol_Consumption,Smoking,Height,Weight,Mood_Level,Date")] Health_Info health_Info)
        {
            health_Info.PId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(health_Info).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            
            ViewBag.PId = new SelectList(db.Personal_Info, "Id", "Fname", health_Info.PId);
            return View(health_Info);
        }

        [Authorize]
        // GET: Health_Info/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Health_Info health_Info = db.Health_Info.Find(id);
            if (health_Info == null)
            {
                return HttpNotFound();
            }
            return View(health_Info);
        }

        // POST: Health_Info/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Health_Info health_Info = db.Health_Info.Find(id);
            db.Health_Info.Remove(health_Info);
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
