using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeClock.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            
            string errorMessage = TempData["ErrorMessage"] as String;
            if (String.IsNullOrEmpty(errorMessage)) { errorMessage = ""; }
            ViewBag.ErrorMessage = errorMessage;
            return View();
        }

        
    }
}