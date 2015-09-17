using NewTech.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewTech.BLL
{
    public class UserManager : BaseManager
    {
        public UserManager(NewTechBll bll) : base(bll)
        {
        }

        public User SelectUser(string userName)
        {
            return _dal.UserRepository.SelectUser(userName);
        }
    }
}
