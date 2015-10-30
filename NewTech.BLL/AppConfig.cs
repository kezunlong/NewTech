using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewTech.BLL
{
    public class AppConfig
    {
        public static string SQLConnString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["SQLConnString"].ConnectionString;
            }
        }

        public static string AppPath
        {
            get
            {
                return ConfigurationManager.AppSettings["AppPath"];
            }
        }
    }
}
