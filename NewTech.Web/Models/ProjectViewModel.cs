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
    }
}