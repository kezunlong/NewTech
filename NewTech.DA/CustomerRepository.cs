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
    public class CustomerRepository : BaseRepository
    {
        public CustomerRepository(NewTechSqlDal dal)
            : base(dal)
        {
        }

        public Customer SelectCustomer(int id)
        {
            Customer item = null;

            sqlProc.Clear();
            sqlProc.CommandText = "SELECT * FROM [dbo].[Customer] WHERE Id = " + id.ToString();
            sqlProc.CommandType = CommandType.Text;
            var table = sqlProc.ExecuteDataTable();
            if(table.Rows.Count > 0)
            {
                item = DataRowToCustomer(table.Rows[0]);
            }

            return item;
        }

        public List<Customer> SelectServicedCustomers()
        {
            List<Customer> list = new List<Customer>();

            sqlProc.Clear();
            sqlProc.CommandText = "SELECT * FROM [dbo].[Customer] c WHERE EXISTS (SELECT * FROM [dbo].[Projects] WHERE Customer = c.Id)";
            sqlProc.CommandType = CommandType.Text;
            var table = sqlProc.ExecuteDataTable();
            foreach (DataRow row in table.Rows)
            {
                list.Add(DataRowToCustomer(row));
            }

            return list;
        }
        public List<string> SelectServicedIndustries()
        {
            List<string> list = new List<string>();

            sqlProc.Clear();
            sqlProc.CommandText = @"SELECT DISTINCT c.Industry FROM [dbo].[Customer] c
JOIN [dbo].[Projects] p ON p.Customer = c.Id";
            sqlProc.CommandType = CommandType.Text;
            var table = sqlProc.ExecuteDataTable();
            foreach (DataRow row in table.Rows)
            {
                var industry = Tools.ConvertString(row["Industry"]);
                if (!string.IsNullOrEmpty(industry))
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

        private Customer DataRowToCustomer(DataRow row)
        {
            return new Customer
            {
                Id = Tools.Convert(row["Id"], 0),
                Name = Tools.ConvertString(row["Name"]),
                Industry = Tools.ConvertString(row["Industry"]),
                Logo = Tools.ConvertString(row["Logo"]),
                ContactPerson = Tools.ConvertString(row["ContactPerson"]),
                Telephone = Tools.ConvertString(row["Telephone"]),
                Mobile = Tools.ConvertString(row["Mobile"]),
                Email = Tools.ConvertString(row["Email"]),
                Address = Tools.ConvertString(row["Address"]),
                Remark = Tools.ConvertString(row["Remark"])
            };
        }
    }
}
