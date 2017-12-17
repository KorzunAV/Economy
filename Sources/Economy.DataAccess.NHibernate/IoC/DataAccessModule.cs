using CQRS.Common;
using Economy.DataAccess.NHibernate.NHibernate;
using Ninject;
using Ninject.Modules;
using Ninject.Parameters;

namespace Economy.DataAccess.NHibernate.IoC
{
    public partial class DataAccessModule : NinjectModule
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

            Bind<IBaseSessionManager, ISessionManager>()
                .To<SessionManager>()
                .InSingletonScope()
                .WithConstructorArgument(new ConstructorArgument("storage", Kernel.GetAll<ISessionStorage>()));

            Bind<IBaseSessionManagerFactory>()
                .To<SessionManagerFactory>()
                .InTransientScope();
        }
    }
}