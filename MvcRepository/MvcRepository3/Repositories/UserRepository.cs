using MvcRepository3.DAL;
using MvcRepository3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcRepository3.Repositories
{
    public class UserRepository<T> : Repository<T> where T : class
    {
        private Context context;
        public UserRepository(Context context):base(context)
        {
            this.context=context;
        }

       public IEnumerable<User> GetHa()
        {
            return context.User.ToList();
        }
    }
}