using System.Data;
using IpagooLibrary.Repository.Infrastructure.Interfaces;
using System.Threading;
using System;

namespace IpagooLibrary.Repository
{
    public class AdoNetContext : IAdoNetContext
    {
        private readonly IDbConnection _connection;
        private readonly IDatabaseFactory _iDatabaseFactory;
        private readonly ReaderWriterLockSlim _rwLock = new ReaderWriterLockSlim();

        public AdoNetContext(IDatabaseFactory iDatabaseFactory)
        {
            _iDatabaseFactory = iDatabaseFactory;
            _connection = _iDatabaseFactory.Create();
        }

        public IDbCommand CreateCommand()
        {
            try
            {
                return _connection.CreateCommand();
            }
            catch (Exception ex)
            {
                //TODO: Log this the exception information along with the method details to the database for Error tracing
                //Allowing the exception be rethrown so that LOG4NET can log there is a problem on the api end point
                return null;
            }

        }

        public IDbCommand CreateCommand(IDbTransaction iDbTransaction)
        {
            try
            {
                var command = _connection.CreateCommand();
                command.Transaction = iDbTransaction;
                return command;
            }
            catch (Exception ex)
            {
                //TODO: Log this the exception information along with the method details to the database for Error tracing
                //Allowing the exception be rethrown so that LOG4NET can log there is a problem on the api end point
                return null;
            }
        }

        public IDbTransaction CreateTransaction()
        {
            try
            {
                // Create a transaction that locks the records of the query
                return _connection.BeginTransaction();
            }
            catch (Exception ex)
            {
                //TODO: Log this the exception information along with the method details to the database for Error tracing
                //Allowing the exception be rethrown so that LOG4NET can log there is a problem on the api end point
                return null;
            }
        }

        public void Dispose()
        {
            try
            {
                _connection.Dispose();
            }
            catch (Exception ex)
            {
                //TODO: Log this the exception information along with the method details to the database for Error tracing
                //Allowing the exception be rethrown so that LOG4NET can log there is a problem on the api end point
            }
        }
    }
}
