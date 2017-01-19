using MvcRepository3.DAL;
using MvcRepository3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcRepository3.Repositories
{
    public class UnitRepository : IDisposable 
    {
        private Context context = new Context();
        private UserRepository<User> userRepository;

        public UserRepository<User> UserRepository
        {
            get 
            { 
                if(userRepository==null)
                {
                    userRepository = new UserRepository<User>(context);
                }
                return userRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }

            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}