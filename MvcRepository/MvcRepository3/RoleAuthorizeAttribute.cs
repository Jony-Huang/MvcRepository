using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcRepository3.DAL;
using MvcRepository3.Models;
using System.Data.SqlClient;
using System.Web.Routing;
using System.Text;

namespace MvcRepository3
{
    public class RoleAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var isAuth = false;

            if(!filterContext.RequestContext.HttpContext.Request.IsAuthenticated) //未登录
            {
                isAuth = false;
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "User", Action = "Login", returnUrl = filterContext.HttpContext.Request.Url, returnMessage = "您未登录" }));
                return;
            }
            else //已经登录
            {
                if (filterContext.RequestContext.HttpContext.User.Identity != null) //已经登录且已经标识
                {
                    var name = filterContext.HttpContext.User.Identity.Name; //当前用户的名称
                    if (name == "admin")
                    {
                        isAuth = true;
                    }
                    else
                    {
                        var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                        var actionName = filterContext.ActionDescriptor.ActionName;

                        //获取当前用户的所有权限
                        using (var db = new Context())
                        {
                            StringBuilder stb = new StringBuilder();
                            stb.Append("SELECT m.*,r.[Description] FROM [User] AS u INNER JOIN UserRole AS ur ON u.ID=ur.UID ");
                            stb.Append("INNER JOIN [Role] AS r ON ur.RID=r.ID INNER JOIN RoleMenu AS rm ");
                            stb.Append("ON rm.RID=r.ID INNER JOIN Menu AS m ON m.ID=rm.MID WHERE u.Name=@name");

                            var menu = db.Database.SqlQuery<Menu>(stb.ToString(), new SqlParameter("@name", name)).ToList();
                            if (menu != null)
                            {
                                isAuth = menu.Any(m => m.Controller == controllerName && m.Action == actionName);
                            }

                        }
                    }
                }

            }

            if (!isAuth)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "User", Action = "Index", returnUrl = filterContext.HttpContext.Request.Url, returnMessage = "您无权查看." }));
                return;
            }
            else
            {
                base.OnAuthorization(filterContext);
            }

            //base.OnAuthorization(filterContext);
        }
    }
}