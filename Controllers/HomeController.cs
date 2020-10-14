using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Team_1_Project.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Core Values";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact Information for Team 1";

            return View();
        }
    }
}