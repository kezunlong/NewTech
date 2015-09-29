using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewTech.Web.Controllers
{
    public class ServiceController : BaseController
    {
        // GET: Service
        public ActionResult OurServices()
        {
            return View();
        }

        public ActionResult ApplicationDevelopment()
        {
            ViewBag.ImportantProjects = bll.ProjectManager.SelectImportantProjects(3);

            return View();
        }
    }
}