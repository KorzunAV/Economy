using System.Configuration;
using BLToolkit.Data;

namespace DBFirstGeneration.DbManagers
{
    internal class PostgreDbBase : DbManager
    {
        private const string ConectionName = "DbConnectionString";
        private const string ConfigurationName = "PostgreDbBaseConfiguration";
        protected const string ProviderName = "PostgreSQL";

        static PostgreDbBase()
        {
            var settings = ConfigurationManager.ConnectionStrings[ConectionName];
            if (settings != null)
            {
                AddConnectionString(ProviderName, ConfigurationName, settings.ConnectionString);
            }
            
            AddDataProvider(ProviderName, new PostgreSqlDataProvider());
        }

        internal PostgreDbBase()
             : base(ProviderName, ConfigurationName)
        { }
    }
}