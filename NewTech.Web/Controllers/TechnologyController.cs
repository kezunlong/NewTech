using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewTech.Web.Controllers
{
    public class TechnologyController : BaseController
    {
        // GET: Technology
        public ActionResult MicrosoftDotNet()
        {
            ViewBag.ImportantProjects = bll.ProjectManager.SelectImportantProjects("1001", 3);

            return View();
        }
    }
}