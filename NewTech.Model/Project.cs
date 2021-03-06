﻿using System;
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
        public int Customer { get; set; }
        public string ThumbImage { get; set; }
        public string Description { get; set; }
        public string Contents { get; set; }
        public bool Status { get; set; }
        public int Order { get; set; }

        public Dict CategoryRef { get; set; }
        public Customer CustomerRef { get; set; }
        public List<Dict> TechnologyRefs { get; set; }

        public string Technologies
        {
            get
            {
                if(TechnologyRefs != null) {
                    return string.Join(", ", TechnologyRefs.Select(x => x.Name));
                }
                else {
                    return string.Empty;
                }
            }
        }

        public Project()
        {
            Status = true;
        }
    }

    public class ProjectFilter
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Industry { get; set; }
        public string Technology { get; set; }
        public bool? Status { get; set; }

        public string GetFilterString()
        {
            string where = string.Empty;

            if(!string.IsNullOrEmpty(Name))
            {
                where += string.Format(" AND Name LIKE '%{0}%'", Name);
            }

            if(!string.IsNullOrEmpty(Category))
            {
                where += string.Format(" AND Category = '{0}'", Category);
            }

            if (!string.IsNullOrEmpty(Industry))
            {
                where += string.Format(" AND EXISTS (SELECT * FROM [dbo].[Customer] WHERE Id = Projects.Customer AND Industry = '{0}')", Industry);
            }

            if(!string.IsNullOrEmpty(Technology))
            {
                where += string.Format(" AND Id IN (SELECT DISTINCT ProjectId FROM [dbo].[ProjectTechnologies] WHERE TechnologyId IN (SELECT Id FROM [dbo].[Dict] WHERE [Parent] = '{0}'))", Technology);
                
            }

            if(Status.HasValue)
            {
                where += string.Format(" AND Status = {0}", Status.Value ? 1 : 0);
            }

            return where;
        }
    }
}
