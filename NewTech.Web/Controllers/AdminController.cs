using Lifepoem.Foundation.Utilities.DBHelpers;
using Lifepoem.Foundation.Utilities.Helpers;
using NewTech.Model;
using NewTech.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewTech.Web.Controllers
{
    public class AdminController : BaseController
    {
        #region Login

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userName, string password, bool? rememberMe)
        {
            string error = string.Empty;
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                error = "用户名或密码不能为空";
            }
            var user = bll.UserManager.SelectUser(userName);
            if(user == null)
            {
                error = "用户不存在";
            }
            else
            {
                if (user.Password != password)
                {
                    error = "用户名或密码不正确";
                }
                else if (!user.Status)
                {
                    error = "用户已失效";
                }
            }

            if(!string.IsNullOrEmpty(error))
            {
                TempData["message"] = new AlertMessage(AlertCategory.Error, error);
                return View();
            }
            else
            {
                return RedirectToAction("Projects");
            }
        }

        #endregion

        #region Projects

        public ActionResult Projects(ProjectFilter filter)
        {
            var model = new ProjectsViewModel
            {
                Filter = filter,
                ServicedApplicationCategories = bll.ProjectManager.SelectServicedApplicationCategories(),
                ServicedIndustries = bll.ProjectManager.SelectServicedIndustries()
            };
            return View(model);
        }

        public ActionResult ProjectsList(ProjectFilter filter, int page = 1)
        {
            PagingOption option = GetPagingOption15(page);

            var model = new ProjectsViewModel
            {
                Projects = bll.ProjectManager.SelectProjects(filter, option),
                PagingOption = new WebPagingOption { CurrentPage = page, ItemsPerPage = PageSize15, TotalItems = option.RecordCount }
            };

            return PartialView(model);
        }


        public ActionResult EditProject(int id)
        {
            var model = new ProjectViewModel { Project = bll.ProjectManager.SelectProject(id) };
            FillAdditionalProperties(model);
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)] 
        public ActionResult EditProject([Bind(Prefix = "Project")]Project item, IEnumerable<string> technologies)
        {
            if (ModelState.IsValid)
            {
                if (item.Id != 0)
                {
                    bll.ProjectManager.UpdateProject(item, technologies);
                }
                else
                {
                    bll.ProjectManager.InsertProject(item, technologies);
                }

                TempData["message"] = new AlertMessage(string.Format("{0} has been saved", item.Name));
                return RedirectToAction("Projects");
            }
            else
            {
                var model = new ProjectViewModel { Project = item };
                FillAdditionalProperties(model);
                return View(model);
            }
        }

        public ActionResult CreateProject()
        {
            var model = new ProjectViewModel { Project = new Project() };
            FillAdditionalProperties(model);
            return View("EditProject", model);
        }

        public ActionResult DeleteProject(int id)
        {
            var item = bll.ProjectManager.DeleteProject(id);
            if (item != null)
            {
                TempData["message"] = new AlertMessage(string.Format("{0} has been deleted", item.Name));
            }
            return RedirectToAction("Projects");
        }

        private void FillAdditionalProperties(ProjectViewModel model)
        {
            model.Categories = bll.DictManager.SelectDicts("Category");
            model.Customers = bll.CustomerManager.SelectCustomers();
            model.TechnologyDicts = bll.DictManager.SelectDicts("Technology");
            ViewBag.DictColumnsType = "Technology";
        }

        #endregion
    }
}