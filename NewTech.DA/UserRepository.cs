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
    public class UserRepository : BaseRepository
    {
        public UserRepository(NewTechSqlDal dal) : base(dal)
        {
        }

        public User SelectUser(string userName)
        {
            User item = null;

            sqlProc.Clear();
            sqlProc.CommandText = "SELECT * FROM [dbo].[User] WHERE UserName = @UserName";
            sqlProc.CommandType = CommandType.Text;
            sqlProc.Parameters.AddWithValue("UserName", userName);
            var table = sqlProc.ExecuteDataTable();
            if(table.Rows.Count > 0)
            {
                item = DataRowToUser(table.Rows[0]);
            }

            return item;
        }

        private User DataRowToUser(DataRow row)
        {
            return new User
            {
                UserName = Tools.ConvertString(row["UserName"]),
                Password = Tools.ConvertString(row["Password"]),
                Role = Tools.ConvertString(row["Role"]),
                Status = Tools.Convert(row["Status"], 0) == 1
            };
        }
    }
}
