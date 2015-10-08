using Lifepoem.Foundation.Utilities.DBHelpers;
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

        public List<Project> SelectProjects(ProjectFilter filter, PagingOption option)
        {
            var list = _dal.ProjectRepository.SelectProjects(filter, option);
            foreach(Project item in list)
            {
                FillReferenceProperties(item);
            }
            return list;
        }

        public List<Project> SelectImportantProjects(string category, int length)
        {
            var list = _dal.ProjectRepository.SelectImportantProjects(category, length);
            foreach (Project item in list)
            {
                FillReferenceProperties(item);
            }
            return list;
        }

        public Project SelectProject(int id)
        {
            var item = _dal.ProjectRepository.SelectProject(id);
            if(item != null)
            {
                FillReferenceProperties(item);
            }
            return item;
        }

        private void FillReferenceProperties(Project item)
        {
            item.CategoryRef = _bll.DictManager.SelectDict(item.Category);
            item.CustomerRef = _bll.CustomerManager.SelectCustomer(item.Customer);
            List<string> technologies = _dal.ProjectRepository.SelectProjectTechnologies(item.Id);
            item.TechnologyRefs = _bll.DictManager.SelectDicts().Where(dict => technologies.Contains(dict.Id)).ToList();
        }

        public List<Dict> SelectServicedIndustries()
        {
            List<string> list = _dal.CustomerRepository.SelectTestServicedIndustries().Take(14).ToList();
            return _bll.DictManager.SelectDicts().Where(dict => list.Contains(dict.Id)).ToList();
        }

        public List<Dict> SelectServicedApplicationCategories()
        {
            List<string> list = _dal.ProjectRepository.SelectServicedApplicationCategories();
            return _bll.DictManager.SelectDicts().Where(dict => list.Contains(dict.Id)).ToList();
        }
        
        public void InsertProject(Project item)
        {
            var existsItem = _dal.ProjectRepository.SelectProject(item.Name);
            if (existsItem != null)
            {
                string message = string.Format("Project (Name = {0}) exists.", item.Name);
                throw new Exception(message);
            }
            _dal.ProjectRepository.InsertProject(item);
        }

        public void UpdateProject(Project item)
        {
            var existsItem = _dal.ProjectRepository.SelectProject(item.Name);
            if (existsItem != null && existsItem.Id != item.Id)
            {
                string message = string.Format("Project (Name = {0}) exists.", item.Name);
                throw new Exception(message);
            }
            _dal.ProjectRepository.UpdateProject(item);
        }

        public Project DeleteProject(int id)
        {
            var item = SelectProject(id);
            if (item != null)
            {
                _dal.ProjectRepository.DeleteProject(id);
            }
            return item;
        }
    }
}
