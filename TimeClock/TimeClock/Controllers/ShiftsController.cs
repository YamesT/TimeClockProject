using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using TimeClock.Models;
using System.Net;

namespace TimeClock.Controllers
{
    public class ShiftsController : Controller
    {
        private EmployeeManagementEntities db = new EmployeeManagementEntities();


        // GET: Shifts
        public ActionResult Index()
        {
            var shifts = db.Shifts.Include(s => s.Employee);
            return View(shifts.ToList());
        }


        public ActionResult Paystub(int? Id)
        {
            var employee = db.Employees.Find(Id);
            var shifts = db.Shifts.Where(s => s.IdentificationNumber == Id).Include(s => s.Employee).ToList();

            TimeSpan ts = new TimeSpan();

            foreach (var shift in shifts)
            {
                if (shift.EndTime.HasValue)
                {
                    ts = ts.Add(shift.EndTime.Value.Subtract(shift.StartTime.Value));
                }
            }
            ViewBag.TotalTime = ts.TotalHours;
            ViewBag.TotalPay = ts.TotalHours * (double)employee.HourlySalary.Value;
            ViewBag.HourRate = employee.HourlySalary.Value;

            return View(shifts);

        }

        // GET: Shifts/Details/S
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Shift shift = db.Shifts.Find(id);

            if (shift == null)
            {
                return HttpNotFound();
            }
            return View(shift);
        }


        // GET: Shifts/Create
        public ActionResult Create()
        {
            ViewBag.IdentificationNumber = new SelectList(db.Employees, "IdenficationNumber", "FirstName");
            return View();
        }


        // POST: Shifts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdentificationNumber, StartTime, EndTime, ShiftID")] Shift shift)
        {
            if (ModelState.IsValid)
            {
                db.Shifts.Add(shift);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdentificationNumber = new SelectList(db.Employees, "IdentificationNumber", "FirstName", shift.IdentificationNumber);
            return View(shift);
        }



        // POST: Shift/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdentificationNumber, StartTime, EndTime, ShiftID")] Shift shift)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shift).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdentificationNumber = new SelectList(db.Employees, "IdentificationNumber", "FirstName", shift.IdentificationNumber);
            return View(shift);
        }


        // GET: Shifts/Delete/5
        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Shift shift = db.Shifts.Find(id);

            if (shift == null)
            {
                return HttpNotFound();
            }
            return View(shift);
        }


        // POST: Shifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Shift shift = db.Shifts.Find(id);
            db.Shifts.Remove(shift);
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