using NewTech.DA;
using NewTech.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewTech.BLL
{
    public class CacheDataManager
    {
        private static CacheDataManager _instance;

        public static CacheDataManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CacheDataManager();
                }
                return _instance;
            }
        }

        #region Dicts

        private List<Dict> dicts;

        internal List<Dict> Dicts
        {
            get
            {
                if (dicts == null)
                {
                    RefreshDicts();
                }
                return dicts;
            }
        }

        internal Dict FindDict(string id)
        {
            return Dicts.Find(item => item.Id == id);
        }

        internal void RefreshDicts()
        {
            using (NewTechSqlDal dal = new NewTechSqlDal(ConfigurationManager.ConnectionStrings["SQLConnString"].ConnectionString))
            {
                dicts = dal.DictRepository.SelectDicts();
            }
        }

        #endregion

    }
}
