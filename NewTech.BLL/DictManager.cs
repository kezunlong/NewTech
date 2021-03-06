﻿using NewTech.Model;
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

        public List<Dict> SelectDicts(string category)
        {
            return CacheDataManager.Instance.Dicts.Where(item => item.Category == category).ToList();
        }

        public List<Dict> SelectDicts(string category, string parent)
        {
            var list = SelectDicts(category);
            return list.Where(item => string.IsNullOrEmpty(parent) ? string.IsNullOrEmpty(item.Parent) : item.Parent == parent).ToList();
        }

        public Dict SelectDict(string id)
        {
            return CacheDataManager.Instance.FindDict(id);
        }
    }
}
