using Ipagoo.ExpressLibary.Repository.Infrastructure.Interfaces;
using Ipagoo.ExpressLibrary.Models.DB;
using System;

namespace Ipagoo.ExpressLibary.Repository.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private readonly string _nameOrConnectionString;
        private DataContext _dataContext;

        public DatabaseFactory(DbConnectionConfig dbConnectionConfig)
        {
            if (dbConnectionConfig == null) throw new ArgumentNullException("No database connection details provided");

            _nameOrConnectionString = dbConnectionConfig.ConnectionString;
        }

        public DataContext Get()
        {
            try
            {
                _dataContext = new DataContext(_nameOrConnectionString);
                return _dataContext;
            }
            catch (Exception ex)
            {
                //Unable to create database context
                return null;
            }
        }

        protected override void DisposeCore()
        {
            if (_dataContext != null)
                _dataContext.Dispose();
        }
    }
}