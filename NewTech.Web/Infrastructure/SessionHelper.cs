using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewTech.Web.Infrastructure
{
    public enum SessionKey
    {
        LOGON_USER
    }

    public static class SessionHelper
    {
        public static void Set(HttpSessionStateBase session, SessionKey key, object value)
        {
            session[Enum.GetName(typeof(SessionKey), key)] = value;
        }

        public static T Get<T>(HttpSessionStateBase session, SessionKey key)
        {
            object dataValue = session[Enum.GetName(typeof(SessionKey), key)];
            if (dataValue != null && dataValue is T)
            {
                return (T)dataValue;
            }
            else
            {
                return default(T);
            }
        }
    }

    public class CookieHelper
    {
        public static string ReadCookie(HttpContext context, string name)
        {
            var cookie = context.Request.Cookies[name];
            if (cookie == null)
            {
                return string.Empty;
            }
            else
            {
                return cookie.Value;
            }
        }

        public static void WriteCookie(HttpContext context, string name, string value)
        {
            HttpCookie cookie = new HttpCookie(name, value);
            cookie.Expires = DateTime.Now.AddDays(14);
            context.Response.Cookies.Add(cookie);
        }
    }
}