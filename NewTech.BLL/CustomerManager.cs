using NewTech.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewTech.BLL
{
    public class CustomerManager : BaseManager
    {
        public CustomerManager(NewTechBll bll)
            : base(bll)
        {
        }

        public Customer SelectCustomer(int id)
        {
            var item = _dal.CustomerRepository.SelectCustomer(id);
            if (item != null)
            {
                item.IndustryRef = _bll.DictManager.SelectDict(item.Industry);
            }
            
            return item;
        }
    }
}
