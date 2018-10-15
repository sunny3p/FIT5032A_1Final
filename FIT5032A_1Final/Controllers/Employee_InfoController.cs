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
    public class Employee_InfoController : Controller
    {
        private FIT5032_A1Final db = new FIT5032_A1Final();

        // GET: Employee_Info
        public ActionResult Index()
        {
            return View(db.Employee_Info.ToList());
        }

        // GET: Employee_Info/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee_Info employee_Info = db.Employee_Info.Find(id);
            if (employee_Info == null)
            {
                return HttpNotFound();
            }
            return View(employee_Info);
        }

        // GET: Employee_Info/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee_Info/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Fname,Lname,Role,DOB,Gender,Contact_No,Address")] Employee_Info employee_Info)
        {
            employee_Info.Id = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Employee_Info.Add(employee_Info);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee_Info);
        }

        // GET: Employee_Info/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee_Info employee_Info = db.Employee_Info.Find(id);
            if (employee_Info == null)
            {
                return HttpNotFound();
            }
            return View(employee_Info);
        }

        // POST: Employee_Info/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fname,Lname,Role,DOB,Gender,Contact_No,Address")] Employee_Info employee_Info)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee_Info).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee_Info);
        }

        // GET: Employee_Info/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee_Info employee_Info = db.Employee_Info.Find(id);
            if (employee_Info == null)
            {
                return HttpNotFound();
            }
            return View(employee_Info);
        }

        // POST: Employee_Info/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Employee_Info employee_Info = db.Employee_Info.Find(id);
            db.Employee_Info.Remove(employee_Info);
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
