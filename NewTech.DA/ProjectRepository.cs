using Lifepoem.Foundation.Utilities;
using NewTech.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewTech.DA
{
    public class ProjectRepository : BaseRepository
    {
        public ProjectRepository(NewTechSqlDal dal) : base(dal) { }

        public List<Project> SelectProjects()
        {
            List<Project> list = new List<Project>();

            sqlProc.Clear();
            sqlProc.CommandText = "SELECT * FROM dbo.Projects";
            sqlProc.CommandType = CommandType.Text;
            var table = sqlProc.ExecuteDataTable();
            foreach(DataRow row in table.Rows)
            {
                list.Add(DataRowToProject(row));
            }

            return list;
        }

        public Project SelectProject(int id)
        {
            Project item = null;

            sqlProc.Clear();
            sqlProc.CommandText = "SELECT * FROM dbo.Projects WHERE Id = " + id.ToString();
            sqlProc.CommandType = CommandType.Text;
            var table = sqlProc.ExecuteDataTable();
            if(table.Rows.Count > 0)
            {
                item = DataRowToProject(table.Rows[0]);
            }

            return item;
        }

        private Project DataRowToProject(DataRow row)
        {
            return new Project
            {
                Id = Tools.Convert(row["Id"], 0),
                Name = Tools.ConvertString(row["Name"]),
                Category = Tools.ConvertString(row["Category"]),
                Industry = Tools.ConvertString(row["Industry"]),
                ThumbImage = Tools.ConvertString(row["ThumbImage"]),
                Description = Tools.ConvertString(row["Description"]),
                Contents = Tools.ConvertString(row["Contents"]),
                Status = Tools.Convert(row["Status"], 0) == 1
            };
        }

        public List<string> SelectProjectTechnologies(int id)
        {
            List<string> list = new List<string>();

            sqlProc.Clear();
            sqlProc.CommandText = "SELECT * FROM [dbo].[ProjectTechnologies] WHERE ProjectId = " + id.ToString();
            sqlProc.CommandType = CommandType.Text;
            var table = sqlProc.ExecuteDataTable();
            foreach (DataRow row in table.Rows)
            {
                var item = Tools.ConvertString(row["TechnologyId"]);
                if (!string.IsNullOrEmpty(item))
                {
                    list.Add(item);
                }
            }

            return list;
        }

        public List<string> SelectServicedIndustries()
        {
            List<string> list = new List<string>();

            sqlProc.Clear();
            sqlProc.CommandText = "SELECT DISTINCT Industry FROM [dbo].[Projects]";
            sqlProc.CommandType = CommandType.Text;
            var table = sqlProc.ExecuteDataTable();
            foreach(DataRow row in table.Rows)
            {
                var industry = Tools.ConvertString(row["Industry"]);
                if(!string.IsNullOrEmpty(industry))
                {
                    list.Add(industry);
                }
            }

            return list;
        }

        public List<string> SelectTestServicedIndustries()
        {
            List<string> list = new List<string>();

            sqlProc.Clear();
            sqlProc.CommandText = "SELECT Id FROM [dbo].[Dict] WHERE Category = 'Industry'";
            sqlProc.CommandType = CommandType.Text;
            var table = sqlProc.ExecuteDataTable();
            foreach (DataRow row in table.Rows)
            {
                var industry = Tools.ConvertString(row["Id"]);
                if (!string.IsNullOrEmpty(industry))
                {
                    list.Add(industry);
                }
            }

            return list;
        }
    }
}
