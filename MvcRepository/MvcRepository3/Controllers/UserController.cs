using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcRepository3.DAL;

namespace MvcRepository3.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            using (var context = new Context())
            {
                var user = context.User.Count();
            }
            return View();
        }

        //登录
        public ActionResult Login()
        {
            //FormsAuthentication.SignOut();
            //TempData["ReturnUrl"] = Convert.ToString(Request["ReturnUrl"]);
            if (HttpContext.User.Identity.IsAuthenticated) {
                return Redirect(Request["returnUrl"]);
            }
            TempData["returnUrl"] = Convert.ToString(Request["returnUrl"]);
            return View();
        }

        [HttpPost]
        
        public ActionResult Login(FormCollection fc)
        {
            //1 获取表单数据
            var name = fc["name"];
            var pwd = fc["pwd"];
            var returnUrl = Convert.ToString(TempData["returnUrl"]);

            //2 验证用户
            using (var db=new Context())
            {
                 var user=db.User.Where(u => u.Name == name && u.PassWord == pwd).FirstOrDefault();
                 if (user != null)
                 {
                     FormsAuthentication.SetAuthCookie(name, false);
                     if (!string.IsNullOrWhiteSpace(returnUrl))
                     {
                         return Redirect(returnUrl);
                     }
                     else
                     {
                         return Redirect("~");
                     }
                 }
                 else
                 {
                     TempData["returnUrl"] = returnUrl;
                     TempData["Message"] = "用户名或密码错误";
                 }
            }

            return View();
        }

        [RoleAuthorize]
        public ActionResult UserCenter()
        {
            return View();
        }

        [RoleAuthorize]
        public ActionResult EditCenter()
        {
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}