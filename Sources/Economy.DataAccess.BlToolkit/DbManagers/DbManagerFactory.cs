using CQRS.Common;

namespace Economy.DataAccess.BlToolkit.DbManagers
{
    public class DbManagerFactory : IBaseSessionManagerFactory
    {
        public IBaseSessionManager GetSession()
        {
            return new EconomyDb();
        }
    }
}