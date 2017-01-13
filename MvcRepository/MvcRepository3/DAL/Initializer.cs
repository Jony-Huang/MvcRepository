using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MvcRepository3.Models;

namespace MvcRepository3.DAL
{
    public class Initializer : DropCreateDatabaseIfModelChanges<Context>
    {
        protected override void Seed(Context context)
        {
            List<User> userList = new List<User>()
            {
                new User { ID = 1, Name = "admin", PassWord = "admin123" },
                new User { ID = 2, Name = "hjf", PassWord = "hjf123" },
                new User { ID = 3, Name = "edit", PassWord = "edit123" },
                new User { ID = 4, Name = "lyb", PassWord = "lyb123" },
                new User { ID = 5, Name = "xml", PassWord = "xml123" }
                
            };

            context.User.AddRange(userList);

            var roleList = new List<Role>() {
                new Role(){ID=1,Name="admin",Description="超级管理员"},
                new Role(){ID=2,Name="general",Description="一般管理员"},
                new Role(){ID=3,Name="editor",Description="编辑"},
                new Role(){ID=4,Name="user",Description="一般用户"}
            };

            context.Role.AddRange(roleList);

            List<Menu> menuList = new List<Menu>(){
                new Menu(){ID=1,Controller="User",Action="Index"},
                new Menu(){ID=2,Controller="User",Action="EditCenter"},
                new Menu(){ID=3,Controller="User",Action="UserCenter"}
            };
            context.Menu.AddRange(menuList);

            List<UserRole> userRoleList = new List<UserRole>() { 
                new UserRole(){ID=1,UID=1,RID=1},
                new UserRole(){ID=2,UID=2,RID=2},
                new UserRole(){ID=3,UID=3,RID=3},
                new UserRole(){ID=4,UID=4,RID=4},
                new UserRole(){ID=5,UID=5,RID=4}
            };
            context.UserRole.AddRange(userRoleList);

            List<RoleMenu> roleMenuList = new List<RoleMenu>() { 
                //超级管理员没有限制

                //一般管理员
                new RoleMenu(){ID=1,RID=2,MID=1},
                new RoleMenu(){ID=2,RID=2,MID=2},
                new RoleMenu(){ID=3,RID=2,MID=3},
                //编辑
                new RoleMenu(){ID=4,RID=3,MID=2},
                new RoleMenu(){ID=5,RID=3,MID=3},
                //一般用户
                new RoleMenu(){ID=6,RID=4,MID=3}, 
            };
            context.RoleMenu.AddRange(roleMenuList);

            context.SaveChanges();

        }
    }
}