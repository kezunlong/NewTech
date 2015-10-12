using Lifepoem.Foundation.Utilities;
using Lifepoem.Foundation.Utilities.DBHelpers;
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

        public List<Project> SelectProjects(ProjectFilter filter, PagingOption option)
        {
            List<Project> list = new List<Project>();

            string sql = "SELECT * FROM dbo.Projects WHERE 1 = 1" + filter.GetFilterString();
            string orderBy = "";
            var table = ExecuteDataTable(sql, option, orderBy);
            foreach (DataRow row in table.Rows)
            {
                list.Add(DataRowToProject(row, false));
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
                item = DataRowToProject(table.Rows[0], true);
            }

            return item;
        }

        public Project SelectProject(string name)
        {
            Project item = null;

            sqlProc.Clear();
            sqlProc.CommandText = "SELECT * FROM dbo.Projects WHERE Name = @Name";
            sqlProc.CommandType = CommandType.Text;
            sqlProc.Parameters.AddWithValue("Name", name);
            var table = sqlProc.ExecuteDataTable();
            if (table.Rows.Count > 0)
            {
                item = DataRowToProject(table.Rows[0], true);
            }

            return item;
        }

        private Project DataRowToProject(DataRow row, bool loadContents)
        {
            return new Project
            {
                Id = Tools.Convert(row["Id"], 0),
                Name = Tools.ConvertString(row["Name"]),
                Category = Tools.ConvertString(row["Category"]),
                Customer = Tools.Convert(row["Customer"], 0),
                ThumbImage = Tools.ConvertString(row["ThumbImage"]),
                Description = Tools.ConvertString(row["Description"]),
                Contents = Tools.ConvertString(row["Contents"]),
                Status = Tools.Convert(row["Status"], 0) == 1,
                Order = Tools.Convert(row["Order"], 0)
            };
        }
                
        public void InsertProject(Project item)
        {
            sqlProc.Clear();
            sqlProc.CommandText = @"INSERT INTO [dbo].[Projects]
    ([Name]
    ,[Category]
    ,[Customer]
    ,[ThumbImage]
    ,[Description]
    ,[Contents]
    ,[Status]
    ,[Order])
VALUES
    (@Name
    ,@Category
    ,@Customer
    ,@ThumbImage
    ,@Description
    ,@Contents
    ,@Status
    ,@Order)";
            sqlProc.CommandType = CommandType.Text;
            AddSqlParameters(item);
            int lastInsertId = 0;
            sqlProc.ExecuteNonQuery(out lastInsertId);
            item.Id = lastInsertId;
        }

        public void UpdateProject(Project item)
        {
            sqlProc.Clear();
            sqlProc.CommandText = @"UPDATE [dbo].[Projects]
SET [Name] = @Name
    ,[Category] = @Category
    ,[Customer] = @Customer
    ,[ThumbImage] = @ThumbImage
    ,[Description] = @Description
    ,[Contents] = @Contents
    ,[Status] = @Status
    ,[Order] = @Order
WHERE Id = @Id";
            sqlProc.CommandType = CommandType.Text;
            AddSqlParameters(item);
            sqlProc.Parameters.AddWithValue("Id", item.Id);
            sqlProc.ExecuteNonQuery();
        }

        public void DeleteProject(int id)
        {
            sqlProc.Clear();
            sqlProc.CommandText = "DELETE FROM [dbo].[Projects] WHERE Id = @Id";
            sqlProc.CommandType = CommandType.Text;
            sqlProc.Parameters.AddWithValue("Id", id);
            sqlProc.ExecuteNonQuery();
        }

        private void AddSqlParameters(Project item)
        {
            sqlProc.Parameters.AddWithValue("Name", item.Name);
            sqlProc.Parameters.AddWithValue("Category", item.Category);
            sqlProc.Parameters.AddWithValue("Customer", item.Customer);
            sqlProc.Parameters.AddWithValue("ThumbImage", item.ThumbImage);
            sqlProc.Parameters.AddWithValue("Description", item.Description);
            sqlProc.Parameters.AddWithValue("Contents", item.Contents);
            sqlProc.Parameters.AddWithValue("Status", item.Status ? 1 : 0);
            sqlProc.Parameters.AddWithValue("Order", item.Order);
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

        public void UpdateProjectTechnologies(int id, IEnumerable<string> technologies)
        {
            foreach(string technology in technologies)
            {
                if(!ExistsProjectTechnology(id, technology))
                {
                    sqlProc.Clear();
                    sqlProc.CommandText = "INSERT INTO [dbo].[ProjectTechnologies]([ProjectId] ,[TechnologyId]) VALUES(@ProjectId, @TechnologyId)";
                    sqlProc.CommandType = CommandType.Text;
                    sqlProc.Parameters.AddWithValue("ProjectId", id);
                    sqlProc.Parameters.AddWithValue("TechnologyId", technology);
                    sqlProc.ExecuteNonQuery();
                }
            }
        }

        private bool ExistsProjectTechnology(int id, string technology)
        {
            sqlProc.Clear();
            sqlProc.CommandText = "SELECT COUNT(*) FROM [dbo].[ProjectTechnologies] WHERE ProjectId = @ProjectId AND TechnologyId = @TechnologyId";
            sqlProc.CommandType = CommandType.Text;
            sqlProc.Parameters.AddWithValue("ProjectId", id);
            sqlProc.Parameters.AddWithValue("TechnologyId", technology);
            return Tools.Convert(sqlProc.ExecuteScalar(), 0) > 0;
        }

        public List<string> SelectServicedApplicationCategories()
        {
            List<string> list = new List<string>();

            sqlProc.Clear();
            sqlProc.CommandText = "SELECT DISTINCT Category FROM [dbo].[Projects]";
            sqlProc.CommandType = CommandType.Text;
            var table = sqlProc.ExecuteDataTable();
            foreach (DataRow row in table.Rows)
            {
                var industry = Tools.ConvertString(row["Category"]);
                if (!string.IsNullOrEmpty(industry))
                {
                    list.Add(industry);
                }
            }

            return list;
        }
    }
}
