using Economy.DataAccess.NHibernate.NHibernate.AdoNet;
using NHibernate.Engine;

namespace NHibernate.AdoNet
{
    public class PostgresClientBatchingBatcherFactory : IBatcherFactory
    {
        public virtual IBatcher CreateBatcher(ConnectionManager connectionManager, IInterceptor interceptor)
        {
            return new PostgresClientBatchingBatcher(connectionManager, interceptor);
        }
    }
}
