using CQRS.Common;
using NHibernate;

namespace Economy.DataAccess.NHibernate.NHibernate
{
    public interface ISessionManager : IBaseSessionManager
    {
        /// <summary>
        /// Current session
        /// </summary>
        ISession CurrentSession { get; }
    }
}
