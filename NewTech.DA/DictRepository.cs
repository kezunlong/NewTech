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
    public class DictRepository : BaseRepository
    {
        public DictRepository(NewTechSqlDal dal) : base(dal) { }

        public List<Dict> SelectDicts()
        {
            List<Dict> list = new List<Dict>();

            sqlProc.Clear();
            sqlProc.CommandText = "SELECT * FROM [dbo].[Dict]";
            sqlProc.CommandType = CommandType.Text;
            var table = sqlProc.ExecuteDataTable();
            foreach (DataRow row in table.Rows)
            {
                list.Add(DataRowToDict(row));
            }

            return list;
        }

        private Dict DataRowToDict(DataRow row)
        {
            return new Dict
            {
                Id = Tools.ConvertString(row["Id"]),
                Category = Tools.ConvertString(row["Category"]),
                Name = Tools.ConvertString(row["Name"]),
                Remark = Tools.ConvertString(row["Remark"]),
                Order = Tools.Convert(row["Order"], 0),
                Status = Tools.Convert(row["Status"], 0) == 1
            };
        }
    }
}
