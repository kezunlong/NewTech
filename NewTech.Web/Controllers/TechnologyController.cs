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
            ViewBag.ImportantProjects = bll.ProjectManager.SelectProjectsByTechnology("1011", 3);

            return View();
        }

        public ActionResult SharepointDevelopment()
        {
            ViewBag.ImportantProjects = bll.ProjectManager.SelectProjectsByTechnology("1012", 3);

            return View();
        }

        public ActionResult JavaDevelopment()
        {
            ViewBag.ImportantProjects = bll.ProjectManager.SelectProjectsByTechnology("1013", 3);

            return View();
        }

        public ActionResult OracleDevelopment()
        {
            ViewBag.ImportantProjects = bll.ProjectManager.SelectProjectsByTechnology("1014", 3);

            return View();
        }
    }
}