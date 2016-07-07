using CQRS.Common;
using Economy.DataAccess.NHibernate.Daos;
using Economy.DataAccess.NHibernate.NHibernate;
using Ninject;
using Ninject.Modules;
using Ninject.Parameters;

namespace Economy.DataAccess.NHibernate.IoC
{
    public class DataAccessModule : NinjectModule
    {
        public override void Load()
        {
            BindImmutable();
            BindDaos();
        }

        private void BindImmutable()
        {
            Bind<ISessionStorage>()
               .To<NHibernateSessionStorage>()
               .InSingletonScope();

            Bind<IBaseSessionManager>()
                .To<SessionManager>()
                .InSingletonScope()
                .WithConstructorArgument(new ConstructorArgument("storage", Kernel.GetAll<ISessionStorage>()));

            var t1 = Kernel.Get<ISessionStorage>();
            var t2 = Kernel.Get<IBaseSessionManager>();
        }
        
        private void BindDaos()
        {
            Bind<BaseDao>()
                .ToSelf()
                .InSingletonScope();

            Bind<BelinvestCourseArhiveDao>()
                .ToSelf()
                .InSingletonScope();
        }

    }
}
