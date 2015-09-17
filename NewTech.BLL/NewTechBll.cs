﻿using NewTech.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewTech.BLL
{
    public class NewTechBll : IDisposable
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        private NewTechSqlDal sqlDAL;
        public NewTechSqlDal SqlDAL
        {
            get
            {
                return sqlDAL;
            }
        }

        #endregion

        #region Constructor

        public NewTechBll(string connectionString)
        {
            sqlDAL = new NewTechSqlDal(connectionString);
        }

        #endregion

        #region Transaction

        public void BeginTransaction()
        {
            sqlDAL.BeginTransaction();
        }

        public void CommitTransaction()
        {
            sqlDAL.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            sqlDAL.RollbackTransaction();
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~NewTechBll()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (sqlDAL != null)
                {
                    sqlDAL.Dispose();
                    sqlDAL = null;
                }
            }
        }

        #endregion

        #region Managers

        private UserManager userManager;
        public UserManager UserManager
        {
            get
            {
                if (userManager == null)
                {
                    userManager = new UserManager(this);
                }
                return userManager;
            }
        }


        #endregion

    }
}
