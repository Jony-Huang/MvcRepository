using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcRepository3.DAL;
using MvcRepository3.Models;
using MvcRepository3.Repositories;

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
                     //1、创建认证信息 Ticket 
                     //使用FormsAuthentication.Encrypt 加密票据。
                     //把加密后的Ticket 存储在Response Cookie中(客户端js不需要读取到这个Cookie，所以最好设置HttpOnly=True，防止浏览器攻击窃取、伪造Cookie)。这样下次可以从Request Cookie中读取了。
                     //FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,uname,DateTime.Now,DateTime.Now.AddMinutes(20),true, "7,1,8", "/" );
                     //var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                     //cookie.HttpOnly = true;
                     //HttpContext.Response.Cookies.Add(cookie);

                     //2、获取认证信息
                    // 登录后，在内容页，我们可以通过，当前请求的User.Identity.Name 获取到uname信息，也可以通过读取Request 中的Cookie 解密，获取到Ticket，再从其中获取uname 和 userData （也就是之前存储的角色ID信息）。
                     //ViewData["user"] = User.Identity.Name;

                     //var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                     //var ticket2 = FormsAuthentication.Decrypt(cookie.Value);
                     //string role = ticket.UserData;


                     //FormsAuthentication.SetAuthCookie(name, true); //48小时有效
                     FormsAuthentication.SetAuthCookie(name, false); //会话cookie
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

        //private IRepository<User> userRepository = new Repository<User>(new Context());
        private UnitRepository unitRepository = new UnitRepository();


        [RoleAuthorize]
        [DescAttrbute(Name="用户中心")]
        public ActionResult UserCenter()
        {
           
            var userList = unitRepository.UserRepository.Get();
            unitRepository.Dispose();
            return View(userList);

            //ReferenceEquals(unitRepository.UserRepository.cc)
            //return View();
        }

        [RoleAuthorize]
        [DescAttrbute(Name = "编辑中心")]
        public ActionResult EditCenter()
        {
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect(Request.UrlReferrer.ToString());
        }


        private void CreatePermissionByController(Controller controller)
        {
            var menuList = new List<Menu>();
            var type = controller.GetType();
            var methods = type.GetMethods();
             
            foreach (var item in methods)
            {
                menuList.Add(new Menu() { Action = item.Name, ActionName = GetPropertyDesc(item) });
            }
        }

        /// <summary>
        /// 根据类型的方法获取方法的特性
        /// </summary>
        /// <param name="method"></param>
        /// <returns>返回方法的特性字符串</returns>
        private string GetPropertyDesc(System.Reflection.MethodInfo method)
        {
            var descAttrbute = (DescAttrbute)(method.GetCustomAttributes(false).FirstOrDefault(x => x is DescAttrbute));
            if (descAttrbute == null) return "";
            return descAttrbute.Name;
        }


    }
}