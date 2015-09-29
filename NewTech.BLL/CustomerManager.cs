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
                FillCustomerProperties(item);
            }
            
            return item;
        }

        public List<Customer> SelectCustomers()
        {
            var list = _dal.CustomerRepository.SelectCustomers();
            foreach(var item in list)
            {
                FillCustomerProperties(item);
            }
            return list;
        }

        private void FillCustomerProperties(Customer item)
        {
            item.IndustryRef = _bll.DictManager.SelectDict(item.Industry);
        }
    }
}
