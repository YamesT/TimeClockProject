using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeClock.Models;
using System.Data.Entity;

namespace TimeClock.Controllers
{
    public class TimeClockController : Controller
    {

        private bool isclockedIn(int employeeID)
        {
            using (var db = new EmployeeManagementEntities())
            {
                var userRecord = db.Shifts.SingleOrDefault(s => s.IdentificationNumber == employeeID && s.EndTime == null && s.StartTime != null);
                return userRecord != null;
            }
        }

       

        // GET: TimeClock
        public ActionResult Clock(string submit)
        {
            var employeeId = int.Parse(Request["EmployeeID"]);

            if(employeeId == null)
            {
                TempData["ErrorMessage"] = "Textbox is empty";
                return RedirectToAction("Index", "Home");
            }
            switch (submit)
            {
                case "Clockin":

                    DateTime clockinTime = DateTime.Now;

                    if(!isclockedIn(employeeId))
                    {
                        using (var db = new EmployeeManagementEntities())
                        {
                            var shiftRecord = new Shift()
                            {
                                IdentificationNumber = employeeId,
                                StartTime = clockinTime,
                                EndTime = null
                            };

                            db.Shifts.Add(shiftRecord);
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Already Clocked in";
                        return RedirectToAction("Index", "Home");
                    }

                    TempData["ErrorMessage"] = "Successfully Clocked in";
                    return RedirectToAction("Index", "Home");



                case "Clockout":

                    DateTime clockoutTime = DateTime.Now;
                    if(isclockedIn(employeeId))
                    {
                        using (var db = new EmployeeManagementEntities())
                        {
                            var userRecord = db.Shifts.SingleOrDefault(s => s.IdentificationNumber == employeeId && s.EndTime == null && s.StartTime != null);
                            if(userRecord != null)
                            {
                                userRecord.EndTime = clockoutTime;

                                db.Entry(userRecord).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Not Clocked in";
                        return RedirectToAction("Index", "Home");
                    }

                    TempData["ErrorMessage"] = "Successfully Clocked out";
                    return RedirectToAction("Index", "Home");


                case "Paystub":
                    return RedirectToAction("Paystub", "Shifts", new { Id = employeeId });

                default: return View();
                    
            }
        }
    }
}