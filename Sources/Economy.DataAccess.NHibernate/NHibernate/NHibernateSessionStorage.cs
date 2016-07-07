using System.Threading;
using System.Web;
using NHibernate;

namespace Economy.DataAccess.NHibernate.NHibernate
{
    public class NHibernateSessionStorage : ISessionStorage
    {
        private const string SESSION_KEY = "47370DC4-ED80-4C4D-8880-FA54BB805847";

        /// <summary>
        /// 	Gets/Sets current NHibernate session.
        /// </summary>
        public ISession CurrentSession
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Items != null)
                {
                    return (ISession)HttpContext.Current.Items[SESSION_KEY];
                }
                return (ISession)Thread.GetData(Thread.GetNamedDataSlot(SESSION_KEY));
            }
            set
            {
                if (HttpContext.Current != null && HttpContext.Current.Items != null)
                {
                    HttpContext.Current.Items[SESSION_KEY] = value;
                }
                else
                {
                    Thread.SetData(Thread.GetNamedDataSlot(SESSION_KEY), value);
                }
            }
        }
    }
}