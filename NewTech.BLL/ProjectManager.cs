using NewTech.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewTech.BLL
{
    public class ProjectManager : BaseManager
    {
        public ProjectManager(NewTechBll bll) : base(bll) { }

        public List<Project> SelectProjects()
        {
            var list = _dal.ProjectRepository.SelectProjects();
            foreach(Project item in list)
            {
                FillReferenceProperties(item);
            }
            return list;
        }

        private void FillReferenceProperties(Project item)
        {
            item.CategoryRef = _bll.DictManager.SelectDict(item.Category);
            item.IndustryRef = _bll.DictManager.SelectDict(item.Industry);
            List<string> technologies = _dal.ProjectRepository.SelectProjectTechnologies(item.Id);
            item.TechnologyRefs = _bll.DictManager.SelectDicts().Where(dict => technologies.Contains(dict.Id)).ToList();
        }

        public List<Dict> SelectServicedIndustries()
        {
            List<string> list = _dal.ProjectRepository.SelectTestServicedIndustries().Take(14).ToList();
            return _bll.DictManager.SelectDicts().Where(dict => list.Contains(dict.Id)).ToList();
        }

    }
}
