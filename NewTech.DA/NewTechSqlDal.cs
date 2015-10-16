using Lifepoem.Foundation.Utilities.DBHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewTech.DA
{
    /// <summary>
    /// Create a Sql Dal class for all the repository clasess.
    /// There are several advantages:
    /// A data access class will use a single connection.
    /// Provide an interface to access various repository classes.
    /// We can define some resources to share among the repository classes, for example, cache data etc.
    /// </summary>
    public class NewTechSqlDal : IDisposable
    {
        #region Properties

        /// <summary>
        /// The Sql Procedure that used to access the database
        /// </summary>
        private SqlProcedure sqlProc;
        public SqlProcedure SqlProc
        {
            get
            {
                return sqlProc;
            }
        }

        #endregion

        #region Constructor

        public NewTechSqlDal(string connectionString)
        {
            sqlProc = new SqlProcedure(connectionString);
            sqlProc.CommandTimeout = 60;
        }

        #endregion

        #region Transaction

        public void BeginTransaction()
        {
            sqlProc.BeginTransaction();
        }

        public void CommitTransaction()
        {
            sqlProc.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            sqlProc.RollbackTransaction();
        }

        #endregion

        #region IDisposable interface

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~NewTechSqlDal()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (sqlProc != null)
                {
                    sqlProc.Dispose();
                    sqlProc = null;
                }
            }
        }

        #endregion

        #region Common

        private UserRepository userRepository;
        public UserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(this);
                }
                return userRepository;
            }
        }

        private CustomerRepository customerRepository;
        public CustomerRepository CustomerRepository
        {
            get
            {
                if (customerRepository == null)
                {
                    customerRepository = new CustomerRepository(this);
                }
                return customerRepository;
            }
        }

        private DictRepository dictRepository;
        public DictRepository DictRepository
        {
            get
            {
                if (dictRepository == null)
                {
                    dictRepository = new DictRepository(this);
                }
                return dictRepository;
            }
        }

        private ProjectRepository projectRepository;
        public ProjectRepository ProjectRepository
        {
            get
            {
                if (projectRepository == null)
                {
                    projectRepository = new ProjectRepository(this);
                }
                return projectRepository;
            }
        }

        private ProposalRepository proposalRepository;
        public ProposalRepository ProposalRepository
        {
            get
            {
                if (proposalRepository == null)
                {
                    proposalRepository = new ProposalRepository(this);
                }
                return proposalRepository;
            }
        }

        #endregion
    }
}
