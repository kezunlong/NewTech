using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewTech.Model
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Industry { get; set; }
        public string ThumbImage { get; set; }
        public string Description { get; set; }
        public string Contents { get; set; }
        public bool Status { get; set; }

        public Dict CategoryRef { get; set; }
        public Dict IndustryRef { get; set; }
        public List<Dict> TechnologyRefs { get; set; } 
    }

    public class ProjectFilter
    {

    }
}
