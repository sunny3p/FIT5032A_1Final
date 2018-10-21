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
    public class ReservationsController : Controller
    {
        private FIT5032_A1Final db = new FIT5032_A1Final();

        [Authorize]
        // GET: Reservations
        public ActionResult Index()
        {
            var id = User.Identity.GetUserId();
            var reservations = db.Reservations.Where(r => r.PId == id);
            //var reservations = db.Reservations.Include(r => r.Employee_Info).Include(r => r.Personal_Info);
            return View(reservations.ToList());
        }

        [Authorize]
        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);

            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        [Authorize]
        // GET: Reservations/Create
        public ActionResult Create()
        {
            ViewBag.EId = new SelectList(db.Employee_Info, "Id", "Fname");
            ViewBag.PId = new SelectList(db.Personal_Info, "Id", "Fname");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "R_Id,R_DateTime,Reason,EId")] Reservation reservation)
        {


            var UserDateTime = db.Reservations.Where(d => d.PId == reservation.PId & d.R_DateTime == reservation.R_DateTime).ToList();

            var AdminDateTime = db.Reservations.Where(d => d.EId == reservation.EId & d.R_DateTime==reservation.R_DateTime).ToList();
            
                if (UserDateTime.Count >= 1)
                {
                    ModelState.AddModelError(string.Empty, "Sorry We couldn't allocate to this booking time. Please check other slots");
                }
                else
                {
                    if (AdminDateTime.Count >= 1)
                    {
                        ModelState.AddModelError(string.Empty, "Sorry We couldn't allocate to this booking time. Please check other slots");
                    }
                    else
                    {
                        ApplicationDbContext dbContext = new ApplicationDbContext();
                        reservation.PId = User.Identity.GetUserId();
                        reservation.R_Status = "Pending";

                        db.Reservations.Add(reservation);
                        db.SaveChanges();
                        var emailId = dbContext.Users.Where(h => h.Id == reservation.PId).First().Email;
                        var id = db.Personal_Info.Where(h => h.Id == reservation.PId).First().Fname;
                        var eid = db.Employee_Info.Where(h => h.Id == reservation.EId).First().Fname;
                        var subject = "Regarding Appointment Details Booking " + reservation.R_Status + "ation";
                        var message = "Hi " + id + " \n This is regarding your appointment with " + eid +
                                      ". Your booking reservation id is " + reservation.R_Id +
                                      " Your current appointment date is" + reservation.R_DateTime +
                                      " and  your current status is " +
                                      reservation.R_Status + ".";
                        EmailController email = new EmailController();
                        email.Send(emailId, subject, message);

                        return RedirectToAction("Index");


                    }

                }
                
                ViewBag.EId = new SelectList(db.Employee_Info, "Id", "Fname", reservation.EId);
                ViewBag.PId = new SelectList(db.Personal_Info, "Id", "Fname", reservation.PId);
                return View(reservation);
            
        }


    
        [Authorize(Roles = "Admin")]
        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Date = db.Reservations.Where(s => s.R_Id == id).FirstOrDefault().R_DateTime.ToShortDateString();
            ViewBag.Time = db.Reservations.Where(s => s.R_Id == id).FirstOrDefault().R_DateTime.ToShortTimeString();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.EId = new SelectList(db.Employee_Info, "Id", "Fname", reservation.EId);
            ViewBag.PId = new SelectList(db.Personal_Info, "Id", "Fname", reservation.PId);
            
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "R_Id,R_DateTime,Reason,PId,R_Status,EId")] Reservation reservation)
        {

            ApplicationDbContext dbContext = new ApplicationDbContext();
            if (ModelState.IsValid)
            {

                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                var emailId = dbContext.Users.Where(h => h.Id == reservation.PId).First().Email;
                var id = db.Personal_Info.Where(h => h.Id == reservation.PId).First().Fname;
                var eid = db.Employee_Info.Where(h => h.Id == reservation.EId).First().Fname;
                var subject = "Regarding Appointment Details Booking " + reservation.R_Status + "ation";
                var message = "Hi " + id + " \n This is regarding your appointment with " + eid +
                              ". Your booking reservation id is " + reservation.R_Id +
                              " Your current appointment date is" + reservation.R_DateTime +
                              " and  your current status is " +
                              reservation.R_Status + ".";
                EmailController email = new EmailController();
                email.Send(emailId, subject, message);

                return RedirectToAction("EmpDetail", "Health_Info");
            }


            ViewBag.EId = new SelectList(db.Employee_Info, "Id", "Fname", reservation.EId);
            ViewBag.PId = new SelectList(db.Personal_Info, "Id", "Fname", reservation.PId);
            return View(reservation);
        }

        [Authorize]
        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
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
