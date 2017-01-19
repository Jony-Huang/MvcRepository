
using NPocoRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPoco;

namespace NPocoRepository.Repositories
{
    public class UserRepository<T> : Repository<T> where T : class
    {
        private Database context;
        public UserRepository(Database context):base(context)
        {
            this.context = context;
        }

        public IEnumerable<User> GetHa()
        {
            return context.Query<User>().ToList();
        }
    }
}