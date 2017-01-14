using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcRepository3.Repositories
{
    interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetByID(object ID);

        void Insert(T t);
        void Delete(object ID);
        void Update(T t);

    }
}