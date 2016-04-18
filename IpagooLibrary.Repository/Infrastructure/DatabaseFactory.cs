using IpagooLibrary.Repository.Infrastructure.Interfaces;
using System;
using System.Data;
using System.Data.Common;
using IpagooLibrary.Models.ErrorHandling;
using IpagooLibrary.Models.DB;

namespace IpagooLibrary.Repository.Infrastructure
{
    public class DatabaseFactory : IDatabaseFactory
    {
        private readonly DbProviderFactory _provider;
        private readonly string _connectionString;
        private readonly string _providerName;

        public DatabaseFactory(DbConnectionConfig dbConnectionConfig)
        {
            if (dbConnectionConfig == null) throw new ArgumentNullException("No database connection details provided");
            _providerName = dbConnectionConfig.ProviderName;
            _provider = DbProviderFactories.GetFactory(dbConnectionConfig.ProviderName);
            _connectionString = dbConnectionConfig.ConnectionString;

        }

        public IDbConnection Create()
        {
            var connection = _provider.CreateConnection();
            if (connection == null)
                throw new ConfigurationErrorsException(string.Format("Failed to create a connection using the connection string named '{0}' in app/web.config.", _providerName));

            connection.ConnectionString = _connectionString;
            connection.Open();
            return connection;
        }
    }
}