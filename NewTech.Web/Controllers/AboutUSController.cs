using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewTech.Web.Controllers
{
    public class AboutUSController : Controller
    {
        // GET: AboutUS
        public ActionResult Company()
        {
            return View();
        }

        public ActionResult WhyNewTech()
        {
            return View();
        }

        public ActionResult VisionAndValue()
        {
            return View();
        }

        public ActionResult ContactUS()
        {
            return View();
        }
    }
}