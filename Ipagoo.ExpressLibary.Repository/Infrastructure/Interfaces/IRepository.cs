using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Ipagoo.ExpressLibary.Repository.Infrastructure.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        T AddReturnId(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        T GetById(long Id);
        T GetById(string Id);
        T Get(Expression<Func<T, bool>> where);
        IList<T> GetAll();
        IList<T> GetMany(Expression<Func<T, bool>> where);
        IEnumerable<T> ExecuteWithStoredProcedure(string query, params object[] parameters);

    }
}