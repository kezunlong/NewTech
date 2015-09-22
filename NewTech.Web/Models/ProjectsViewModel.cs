using Lifepoem.Foundation.Utilities.Helpers;
using NewTech.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewTech.Web.Models
{
    public class ProjectsViewModel
    {
        public ProjectFilter Filter { get; set; }
        public IEnumerable<Project> Projects { get; set; }
        public WebPagingOption PagingOption { get; set; }
        public IEnumerable<Dict> ServicedIndustries { get; set; }
        public IEnumerable<Dict> ServicedApplicationCategories { get; set; }
        public IEnumerable<Dict> Technologies { get; set; }
    }
}