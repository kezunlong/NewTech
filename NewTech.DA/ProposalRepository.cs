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
    public class ProposalRepository : BaseRepository
    {
        public ProposalRepository(NewTechSqlDal dal)
            : base(dal)
        {
        }

        public Proposal SelectProposal(int id)
        {
            Proposal item = null;

            sqlProc.Clear();
            sqlProc.CommandText = "SELECT * FROM [dbo].[Proposal] WHERE Id = " + id.ToString();
            sqlProc.CommandType = CommandType.Text;
            var table = sqlProc.ExecuteDataTable();
            if (table.Rows.Count > 0)
            {
                item = DataRowToProposal(table.Rows[0]);
            }

            return item;
        }

        public List<Proposal> SelectProposals(ProposalFilter filter, PagingOption option)
        {
            List<Proposal> list = new List<Proposal>();

            string sql = "SELECT * FROM dbo.Proposal WHERE 1 = 1" + filter.GetFilterString();
            string orderBy = "";
            var table = ExecuteDataTable(sql, option, orderBy);
            foreach (DataRow row in table.Rows)
            {
                list.Add(DataRowToProposal(row));
            }

            return list;
        }

        public void InsertProposal(Proposal item)
        {
            sqlProc.Clear();
            sqlProc.CommandText = @"INSERT INTO [dbo].[Proposal]
    ([Name]
    ,[JobTitle]
    ,[Email]
    ,[Telephone]
    ,[CompanyName]
    ,[ProjectTitle]
    ,[Technology]
    ,[Description]
    ,[OtherComments]
    ,[UploadDocument1]
    ,[UploadDocument2])
VALUES
    (@Name
    ,@JobTitle
    ,@Email
    ,@Telephone
    ,@CompanyName
    ,@ProjectTitle
    ,@Technology
    ,@Description
    ,@OtherComments
    ,@UploadDocument1
    ,@UploadDocument2)";
            sqlProc.CommandType = CommandType.Text;
            AddSqlParameters(item);
            sqlProc.ExecuteNonQuery();
        }

        public void UpdateProposal(Proposal item)
        {
            sqlProc.Clear();
            sqlProc.CommandText = @"UPDATE [dbo].[Proposal]
SET [Name] = @Name
    ,[JobTitle] = @JobTitle
    ,[Email] = @Email
    ,[Telephone] = @Telephone
    ,[CompanyName] = @CompanyName
    ,[ProjectTitle] = @ProjectTitle
    ,[Technology] = @Technology
    ,[Description] = @Description
    ,[OtherComments] = @OtherComments
    ,[UploadDocument1] = @UploadDocument1
    ,[UploadDocument2] = @UploadDocument2
WHERE Id = @Id";
            sqlProc.CommandType = CommandType.Text;
            AddSqlParameters(item);
            sqlProc.Parameters.AddWithValue("Id", item.Id);
            sqlProc.ExecuteNonQuery();
        }

        public void DeleteProposal(int id)
        {
            sqlProc.Clear();
            sqlProc.CommandText = "DELETE FROM [dbo].[Proposal] WHERE Id = " + id.ToString();
            sqlProc.CommandType = CommandType.Text;
            sqlProc.ExecuteNonQuery();
        }

        private void AddSqlParameters(Proposal item)
        {
            sqlProc.Parameters.AddWithValue("Name", item.Name);
            sqlProc.Parameters.AddWithValue("JobTitle", item.JobTitle);
            sqlProc.Parameters.AddWithValue("Email", item.Email);
            sqlProc.Parameters.AddWithValue("Telephone", item.Telephone);
            sqlProc.Parameters.AddWithValue("CompanyName", item.CompanyName);
            sqlProc.Parameters.AddWithValue("ProjectTitle", item.ProjectTitle);
            sqlProc.Parameters.AddWithValue("Technology", item.Technology);
            sqlProc.Parameters.AddWithValue("Description", item.Description);
            sqlProc.Parameters.AddWithValue("OtherComments", item.OtherComments);
            sqlProc.Parameters.AddWithValue("UploadDocument1", item.UploadDocument1);
            sqlProc.Parameters.AddWithValue("UploadDocument2", item.UploadDocument2);
        }

        private Proposal DataRowToProposal(DataRow row)
        {
            return new Proposal
            {
                Id = Tools.Convert(row["Id"], 0),
                Name = Tools.ConvertString(row["Name"]),
                JobTitle = Tools.ConvertString(row["JobTitle"]),
                Email = Tools.ConvertString(row["Email"]),
                Telephone = Tools.ConvertString(row["Telephone"]),
                CompanyName = Tools.ConvertString(row["CompanyName"]),
                ProjectTitle = Tools.ConvertString(row["ProjectTitle"]),
                Technology = Tools.ConvertString(row["Technology"]),
                Description = Tools.ConvertString(row["Description"]),
                OtherComments = Tools.ConvertString(row["OtherComments"]),
                UploadDocument1 = Tools.ConvertString(row["UploadDocument1"]),
                UploadDocument2 = Tools.ConvertString(row["UploadDocument2"])
            };
        }
    }
}
