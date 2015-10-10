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
    public class PortfolioController : BaseController
    {
        // GET: Portfolio
        public ActionResult Index(ProjectFilter filter)
        {
            var model = new ProjectsViewModel
            {
                Filter = filter,
                ServicedApplicationCategories = bll.ProjectManager.SelectServicedApplicationCategories(),
                ServicedIndustries = bll.ProjectManager.SelectServicedIndustries(),
                Technologies = bll.DictManager.SelectDicts("Technology", string.Empty)
            };
            ViewBag.PortfolioTitle = GetPortfolioTitle(filter);
            return View(model);
        }

        public ActionResult PortfolioList(ProjectFilter filter, int page = 1)
        {
            PagingOption option = GetPagingOption(page);

            var model = new ProjectsViewModel
            {
                Projects = bll.ProjectManager.SelectProjects(filter, option),
                PagingOption = new WebPagingOption { CurrentPage = page, ItemsPerPage = PageSize, TotalItems = option.RecordCount }
            };

            return PartialView(model);
        }

        private string GetPortfolioTitle(ProjectFilter filter)
        {
            string title = "案例展示";
            if(!string.IsNullOrEmpty(filter.Industry))
            {
                title = bll.DictManager.SelectDict(filter.Industry).Name;
            }
            else if (!string.IsNullOrEmpty(filter.Category))
            {
                title = bll.DictManager.SelectDict(filter.Category).Name;
            }
            else if(!string.IsNullOrEmpty(filter.Technology))
            {
                title = bll.DictManager.SelectDict(filter.Technology).Name;
            }
            return title;
        }

        public ActionResult ProjectsList(ProjectFilter filter, int page = 1)
        {
            PagingOption option = GetPagingOption(page);

            var model = new ProjectsViewModel
            {
                Projects = bll.ProjectManager.SelectProjects(filter, option),
                PagingOption = new WebPagingOption { CurrentPage = page, ItemsPerPage = PageSize, TotalItems = option.RecordCount }
            };

            return PartialView(model);
        }

        //public PartialViewResult PortfolioThumb(int id)
        //{
        //    Project item = bll.ProjectManager.SelectProject(id);
        //    return PartialView(item);
        //}
    }
}