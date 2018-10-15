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
    public class Personal_InfoController : Controller
    {
        private FIT5032_A1Final db = new FIT5032_A1Final();

        // GET: Personal_Info
        public ActionResult Index()
        {
            return View(db.Personal_Info.ToList());
        }

        // GET: Personal_Info/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personal_Info personal_Info = db.Personal_Info.Find(id);
            if (personal_Info == null)
            {
                return HttpNotFound();
            }
            return View(personal_Info);
        }

        // GET: Personal_Info/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Personal_Info/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Fname,Lname,DOB,Gender,Contact_No,Address")] Personal_Info personal_Info)
        {
            personal_Info.Id = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Personal_Info.Add(personal_Info);
                db.SaveChanges();
                return RedirectToAction("Index","Health_Info");
            }

            return View(personal_Info);
        }

        // GET: Personal_Info/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personal_Info personal_Info = db.Personal_Info.Find(id);
            if (personal_Info == null)
            {
                return HttpNotFound();
            }
            return View(personal_Info);
        }

        // POST: Personal_Info/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fname,Lname,DOB,Gender,Contact_No,Address")] Personal_Info personal_Info)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personal_Info).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(personal_Info);
        }

        // GET: Personal_Info/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personal_Info personal_Info = db.Personal_Info.Find(id);
            if (personal_Info == null)
            {
                return HttpNotFound();
            }
            return View(personal_Info);
        }

        // POST: Personal_Info/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Personal_Info personal_Info = db.Personal_Info.Find(id);
            db.Personal_Info.Remove(personal_Info);
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
