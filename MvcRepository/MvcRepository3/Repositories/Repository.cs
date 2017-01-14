using MvcRepository3.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
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

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,  string includeProperties = "")
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query.Where(filter);
            }

            foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(item);
            }

            if (orderBy != null)
            {
                //if(isDesc)
                //{
                    
                //}
                //else
                //{
                //    return orderBy(query).ToList();
                //}
                return orderBy(query).ToList();
                
            }
            else
            {
                return query.ToList();
            }

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