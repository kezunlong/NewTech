using NewTech.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Web;
using System.Web.Mvc;

namespace NewTech.Web.Infrastructure
{
    //[Flags]
    //public enum Permissions
    //{
    //    View = (1 << 0),
    //    Add = (1 << 1),
    //    Edit = (1 << 2),
    //    Delete = (1 << 3),
    //    Admin = (View | Add | Edit | Delete)
    //}

    public enum Permissions
    {
        Administrator
    }

    public class PermissionsAttribute : ActionFilterAttribute
    {
        private readonly Permissions required;

        public PermissionsAttribute(Permissions required)
        {
            this.required = required;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            User user = SessionHelper.Get<User>(filterContext.HttpContext.Session, SessionKey.LOGON_USER);
            if (user == null)
            {
                //send them off to the login page
                var url = new UrlHelper(filterContext.RequestContext);
                var loginUrl = url.Content("~/Admin/Login");
                filterContext.HttpContext.Response.Redirect(loginUrl, true);
            }
            else
            {
                if (!(user.Role == required.ToString()))
                {
                    throw new AuthenticationException("You do not have the necessary permission to perform this action");
                }
            }
        }
    }

}