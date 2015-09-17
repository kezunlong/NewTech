using NewTech.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewTech.BLL
{
    public class DictManager : BaseManager
    {
        public DictManager(NewTechBll bll) : base(bll) { }

        public List<Dict> SelectDicts()
        {
            return CacheDataManager.Instance.Dicts;
        }

        public Dict SelectDict(string id)
        {
            return CacheDataManager.Instance.FindDict(id);
        }
    }
}
