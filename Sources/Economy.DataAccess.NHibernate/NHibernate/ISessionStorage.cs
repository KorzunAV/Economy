using NHibernate;

namespace Economy.DataAccess.NHibernate.NHibernate
{
    /// <summary>
    /// 	Represents NHibernate session storage interface.
    /// </summary>
    public interface ISessionStorage
    {
        /// <summary>
        /// 	Gets/Sets current NHibernate session.
        /// </summary>
        ISession CurrentSession { get; set; }
    }
}