using System.Data;
using CQRS.Common;

namespace Economy.DataAccess.BlToolkit.DbManagers
{
    internal partial class EconomyDb : PostgreDbBase, IBaseSessionManager
    {
        private const string ConfigurationName = "EconomyDbConfiguration";
        private const string ConnectionString = "Server=localhost;Port=5432;Database=economy; User Id=postgres;Password=1234;Pooling=False;";
        

        static EconomyDb()
        {
            AddConnectionString(ProviderName, ConfigurationName, ConnectionString);
        }

        public EconomyDb() : base(ConfigurationName) { }


        public void OpenSession()
        {

        }

        public void CloseSession()
        {

        }

        public new void BeginTransaction(IsolationLevel isolationLevel)
        {
            base.BeginTransaction(isolationLevel);
        }

        public new void CommitTransaction()
        {
            base.CommitTransaction();
        }

        public new void RollbackTransaction()
        {
            base.RollbackTransaction();
        }
    }
}