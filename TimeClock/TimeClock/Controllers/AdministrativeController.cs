using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeClock.Models;

namespace TimeClock.Controllers
{
    public class AdministrativeController : Controller
    {
        // GET: Administrative
        public ActionResult Login()
        {
            string userName = Request["Username"].ToString();
            string password = Request["Password"].ToString();



            using (var db = new EmployeeManagementEntities())
            {


                var userRecord = db.Administrators.SingleOrDefault(s => s.userName.Equals(userName));


                if (userRecord == null)
                {
                    // Display an error
                    //= "There was an error with your username or password";
                    //return;

                    TempData["ErrorMessage"] = "Username and password don't match";
                    return RedirectToAction("Index", "Home");
                }

                if (!userRecord.passWord.Equals(password))
                {
                    // Diplsay wrong password
                    //lblLogin.Text = "There was an error with your username or password";
                    //return;
                    TempData["ErrorMessage"] = "Username and password don't match";
                    return RedirectToAction("Index", "Home");
                }

                // If you get here, login is successful, go to the registration page
                //lblLogin.Text = "";
                //Session["student"] = userRecord;
                TempData["ErrorMessage"] = "";
                return RedirectToAction("Create", "Employees");


            }
        }
    }
}