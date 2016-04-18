using Ipagoo.ExpressLibrary.Models.DB;
using System.Configuration;


namespace Ipagoo.ExpressLibrary.Api
{
    public static class AppConfig
    {
        public static DbConnectionConfig ConnectionString { get { return GetConnectionString("LibraryExpressDbContext"); } }

        private static T GetConfigValue<T>(string identifier) where T : class
        {
            var appsettingValue = ConfigurationManager.AppSettings[identifier] as T;

            if (appsettingValue == null) throw new ConfigurationErrorsException(string.Format("Failed to find value '{0}' in app/web.config.", identifier));

            return appsettingValue;
        }

        private static DbConnectionConfig GetConnectionString(string identifier)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[identifier];
            if (connectionString == null) throw new ConfigurationErrorsException(string.Format("Failed to find connection string named '{0}' in app/web.config.", identifier));

            return new DbConnectionConfig
            {
                ProviderName = connectionString.ProviderName,
                ConnectionString = connectionString.ConnectionString
            };
        }
    }
}