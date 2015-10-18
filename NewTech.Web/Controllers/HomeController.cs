using NewTech.Model;
using NewTech.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
            return View(new Proposal());
        }

        [HttpPost]
        public ActionResult RequestForProposal(Proposal item, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                if (files != null && files.Count() > 0)
                {
                    UploadFilesForProposal(item, files);
                }

                if (item.Id != 0)
                {
                    bll.ProposalManager.UpdateProposal(item);
                }
                else
                {
                    bll.ProposalManager.InsertProposal(item);
                }

                TempData["message"] = new AlertMessage(string.Format("{0} has been saved", item.Name));
                return RedirectToAction("ProposalSucceed");
            }
            else
            {
                return View(item);
            }
        }

        private void UploadFilesForProposal(Proposal item, IEnumerable<HttpPostedFileBase> files)
        {
            int index = 0;
            foreach(var file in files)
            {
                if (file != null && file.ContentLength > 0) 
                { 
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/uploads/proposal"), fileName); 
                    file.SaveAs(path);

                    var webRelativeFile = "App_Data/uploads/proposal/" + fileName;
                    if (index == 0)
                    {
                        item.UploadDocument1 = webRelativeFile;
                    }
                    else if (index == 1)
                    {
                        item.UploadDocument2 = webRelativeFile;
                    }
                }
                index++;
            }
        }

        public ActionResult ProposalSucceed()
        {
            return View();
        }
    }
}