using Lifepoem.Foundation.Utilities.DBHelpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewTech.DA
{
    public abstract class BaseRepository
    {
        #region Fields

        /// <summary>
        /// The Sql Procedure that used to access the database
        /// </summary>
        protected NewTechSqlDal _dal;

        protected SqlProcedure sqlProc
        {
            get
            {
                return _dal.SqlProc;
            }
        }

        #endregion

        #region Constructor

        public BaseRepository(NewTechSqlDal dal)
        {
            this._dal = dal;
        }

        #endregion


        protected DataTable ExecuteDataTable(string sql, PagingOption option, string orderBy = null)
        {
            if (option == null)
            {
                sqlProc.Clear();
                sqlProc.CommandText = sql + (string.IsNullOrEmpty(orderBy) ? "" : " ORDER BY " + orderBy);
                sqlProc.CommandType = CommandType.Text;
                return sqlProc.ExecuteDataTable();
            }
            else
            {
                sqlProc.Clear();
                sqlProc.CommandText = sql;
                sqlProc.CommandType = CommandType.Text;
                option.OrderBy = orderBy;
                return sqlProc.GetPagingRecord(option);
            }

        }
    }
}
