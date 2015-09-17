using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewTech.Model
{
    public class User
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastLogonDate { get; set; }

        public string Role { get; set; }

        public bool Status { get; set; }
    }
}
