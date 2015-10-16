using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewTech.Web.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.ServicedIndustries = bll.ProjectManager.SelectServicedIndustries();
            ViewBag.DictColumnsType = "Industry";

            return View();
        }

        public ActionResult RequestForProposal()
        {
            return View();
        }
    }
}