using NewTech.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewTech.Web.Controllers
{
    public class AdminController : BaseController
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userName, string password, bool? rememberMe)
        {
            string error = string.Empty;
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                error = "用户名或密码不能为空";
            }
            var user = bll.UserManager.SelectUser(userName);
            if(user == null)
            {
                error = "用户不存在";
            }
            else
            {
                if (user.Password != password)
                {
                    error = "用户名或密码不正确";
                }
                else if (!user.Status)
                {
                    error = "用户已失效";
                }
            }

            if(!string.IsNullOrEmpty(error))
            {
                TempData["message"] = new AlertMessage(AlertCategory.Error, error);
                return View();
            }
            else
            {
                return RedirectToAction("Projects");
            }
        }

        public ActionResult Projects()
        {

            return View();
        }
    }
}