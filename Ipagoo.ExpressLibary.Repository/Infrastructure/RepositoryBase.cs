using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using Ipagoo.ExpressLibary.Repository.Infrastructure.Interfaces;

namespace Ipagoo.ExpressLibary.Repository.Infrastructure
{
    public abstract class RepositoryBase<T> where T : class
    {
        private DataContext _dataContext;
        private readonly IDbSet<T> _dbset;

        protected RepositoryBase(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            _dbset = DataContext.Set<T>();
            DataContext.Database.Log = Logger;
        }

        protected IDatabaseFactory DatabaseFactory { get; private set; }

        protected DataContext DataContext
        {
            get { return _dataContext ?? (_dataContext = DatabaseFactory.Get()); }
        }

        public virtual void Add(T entity)
        {
            _dbset.Add(entity);
        }

        public virtual T AddReturnId(T entity)
        {
            _dbset.Add(entity);
            _dataContext.SaveChanges();
            return entity;
        }
        public virtual void Update(T entity)
        {
            _dbset.Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            _dbset.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            var objects = _dbset.Where(where).AsEnumerable();
            foreach (var obj in objects)
                _dbset.Remove(obj);
        }

        public virtual T GetById(long id)
        {
            return _dbset.Find(id);
        }

        public virtual T GetById(string id)
        {
            return _dbset.Find(id);
        }

        public virtual IList<T> GetAll()
        {
            return _dbset.ToList();
        }

        public virtual IList<T> GetMany(Expression<Func<T, bool>> where)
        {
            return _dbset.Where(where).ToList();
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return _dbset.Where(where).FirstOrDefault();
        }
        private void Logger(string logString)
        {
            Debug.WriteLine(logString);
        }

        public IEnumerable<T> ExecuteWithStoredProcedure(string query, params object[] parameters)
        {
            return _dataContext.Database.SqlQuery<T>(query, parameters);
        }
    }
}