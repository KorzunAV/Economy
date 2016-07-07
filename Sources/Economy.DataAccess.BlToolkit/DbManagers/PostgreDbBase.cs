using BLToolkit.Data;

namespace Economy.DataAccess.BlToolkit.DbManagers
{
    public abstract class PostgreDbBase : DbManager
    {
        private const string ConfigurationName = "PostgreDbBaseConfiguration";
        protected const string ProviderName = "PostgreSQL";

        static PostgreDbBase()
        {
            AddDataProvider(ProviderName, new PostgreSqlDataProvider());
        }

        protected PostgreDbBase()
            : this(ConfigurationName)
        { }

        protected PostgreDbBase(string configuration)
            : base(ProviderName, configuration)
        {
            // Command.CommandTimeout = 120 * 60;
            //http://blogs.rsdn.ru/it/3716262
        }
    }
}