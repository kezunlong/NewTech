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

        public static string UploadPath
        {
            get
            {
                return ConfigurationManager.AppSettings["UploadPath"];
            }
        }

        public static string UploadURL
        {
            get
            {
                return ConfigurationManager.AppSettings["UploadURL"];
            }
        }

        public static string NotificationEmail
        {
            get
            {
                return ConfigurationManager.AppSettings["NotificationEmail"];
            }
        }

        public static string SMTPServer
        {
            get
            {
                return ConfigurationManager.AppSettings["SMTPServer"];
            }
        }
        
        public static string MailFrom
        {
            get
            {
                return ConfigurationManager.AppSettings["MailFrom"];
            }
        }

        public static string MailFromPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["MailFromPassword"];
            }
        }
    }
}
