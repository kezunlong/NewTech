using Lifepoem.Foundation.Utilities.Helpers;
using NewTech.BLL;
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

        public ActionResult RequestForProposal(int id = 0)
        {
            Proposal item;
            if (id != 0)
            {
                item = bll.ProposalManager.SelectProposal(id);
            }
            else
            {
                item = new Proposal();
            }
            return View(item);
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

                    TempData["message"] = new AlertMessage(string.Format("{0} has been saved", item.Name));
                    return RedirectToAction("Proposals", "Admin");
                }
                else
                {
                    bll.ProposalManager.InsertProposal(item);

                    SendProposalNotification(item);

                    TempData["message"] = new AlertMessage(string.Format("{0} has been saved", item.Name));
                    return RedirectToAction("ProposalSucceed");
                }
            }
            else
            {
                return View(item);
            }
        }

        private void SendProposalNotification(Proposal item)
        {
            string body = System.IO.File.ReadAllText(Server.MapPath("~/App_Data/MailTemplate/ProposalMailTemplate.html"), System.Text.Encoding.UTF8);
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("{Name}", item.Name);
            values.Add("{JobTitle}", item.JobTitle);
            values.Add("{Email}", item.Email);
            values.Add("{Telephone}", item.Telephone);
            values.Add("{CompanyName}", item.CompanyName);
            values.Add("{ProjectTitle}", item.ProjectTitle);
            values.Add("{Technology}", item.Technology);
            values.Add("{UploadDocument1}", item.UploadDocument1);
            values.Add("{UploadDocument2}", item.UploadDocument2);
            values.Add("{Description}", item.Description);
            values.Add("{OtherComments}", item.OtherComments);
            values.Add("{URL}", AppConfig.AppPath + "/Home/RequestForProposal/" + item.Id.ToString());

            string subject = string.Format("投标申请/报价咨询：{0}", item.ProjectTitle);
            MailHelper.Send(AppConfig.MailFrom, AppConfig.MailFromPassword, AppConfig.NotificationEmail, subject, body, true, AppConfig.SMTPServer, values);
        }

        private void UploadFilesForProposal(Proposal item, IEnumerable<HttpPostedFileBase> files)
        {
            int index = 0;
            foreach(var file in files)
            {
                if (file != null && file.ContentLength > 0) 
                { 
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(AppConfig.UploadPath, "proposal", fileName); 
                    file.SaveAs(path);

                    var fileURL = CombineURL(AppConfig.UploadURL, "proposal", fileName);
                    if (index == 0)
                    {
                        item.UploadDocument1 = fileURL;
                    }
                    else if (index == 1)
                    {
                        item.UploadDocument2 = fileURL;
                    }
                }
                index++;
            }
        }

        private string CombineURL(string path1, string path2)
        {
            string result = path1;
            string splitter = "/";
            if (!result.EndsWith(splitter))
            {
                result += splitter;
            }
            result += path2;
            return result;
        }

        private string CombineURL(string path1, string path2, string path3)
        {
            string result = CombineURL(path1, path2);
            result = CombineURL(result, path3);
            return result;
        }

        public ActionResult ProposalSucceed()
        {
            return View();
        }
    }
}