using CQRS.Common;

namespace Economy.DataAccess.NHibernate.NHibernate
{
    public class SessionManagerFactory : IBaseSessionManagerFactory
    {
        IBaseSessionManager _baseSessionManager;

        public SessionManagerFactory(IBaseSessionManager baseSessionManager)
        {
            _baseSessionManager = baseSessionManager;
        }

        public IBaseSessionManager GetSession()
        {
            return _baseSessionManager;
        }
    }
}