
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using NPoco;
using NPoco.Linq;

namespace NPocoRepository.Repositories
{
    public class Repository<T> :IRepository<T> where T : class
    {
        private Database context;
        public Repository(Database context)
        {
            this.context = context;
        }

        //public Database Context { get { return this.context; } }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryProvider<T>, IQueryProvider<T>> orderBy = null, string includeProperties = "")
        {
            var query = context.Query<T>();
            if (filter != null)
            {
                query.Where(filter);
            }
            
            //foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            //{
            //    query = query.Include()
            //}

            if (orderBy != null)
            {
                return orderBy(query).ToList();

            }
            //else
            //{
            //    return query.ToList();
            //}
            return query.ToList();
        }

        public void Insert(T t)
        {
            this.context.Insert<T>(t);
        }

        public void Delete(object ID)
        {
            this.context.Delete<T>(ID);
        }

        public virtual void Delete(T t)
        {
            context.Delete<T>(t);
        }

        public void Update(T t)
        {
            this.context.Update(t);
        }

        
    }
}