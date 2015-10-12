using NewTech.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewTech.Web.Models
{
    public class ProjectViewModel
    {
        public Project Project { get; set; }
        public IEnumerable<Dict> Categories { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<Dict> ServicedIndustries { get; set; }
        public IEnumerable<Dict> ServicedApplicationCategories { get; set; }
        public IEnumerable<Dict> Technologies { get; set; }

        #region Used for Project Edit
        public string ParentTechnology { get; set; }
        public IEnumerable<string> SelectedTechnologies { get; set; }
        public IEnumerable<Dict> TechnologyDicts { get; set; }
        #endregion
    }
}