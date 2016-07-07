using CQRS.Common;

namespace Economy.DataAccess.NHibernate.Daos
{
    public partial class BelinvestCourseArhiveDao : BaseDao
    {
        public BelinvestCourseArhiveDao(IBaseSessionManager sessionManager)
            : base(sessionManager) { }

        //BelinvestCourseArhiveDao.Command
        //BelinvestCourseArhiveDao.Query
    }
}