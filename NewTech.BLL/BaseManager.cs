using NewTech.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewTech.BLL
{
    public abstract class BaseManager
    {
        /// <summary>
        /// the data access layer of the dev database
        /// </summary>
        protected NewTechBll _bll;

        /// <summary>
        /// the data access layer of the dev database
        /// </summary>
        protected NewTechSqlDal _dal;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public BaseManager(NewTechBll bll)
        {
            this._bll = bll;
            this._dal = bll.SqlDAL;
        }

    }
}
