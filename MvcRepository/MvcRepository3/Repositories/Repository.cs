using MvcRepository3.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcRepository3.Repositories
{
    public class Repository<T> :IRepository<T> where T : class
    {
        private Context context;
        internal DbSet<T> dbSet;
        public Repository(Context context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public Context Context { get { return this.context; } }

        public IEnumerable<T> GetAll()
        {
            return this.dbSet.ToList();
        }

        public T GetByID(object ID)
        {
            return this.dbSet.Find(ID);
        }

        public void Insert(T t)
        {
            this.dbSet.Add(t);
        }

        public void Delete(object ID)
        {
            T t = this.dbSet.Find(ID);
            Delete(t);
        }

        public virtual void Delete(T t)
        {
            if (context.Entry(t).State == EntityState.Detached)
            {
                this.dbSet.Attach(t);
            }
            this.dbSet.Remove(t);
        }

        public void Update(T t)
        {
            this.dbSet.Attach(t);
            this.context.Entry(t).State = EntityState.Modified;
        }

        
    }
}