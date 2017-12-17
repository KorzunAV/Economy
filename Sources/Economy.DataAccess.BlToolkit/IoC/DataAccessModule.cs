using CQRS.Common;
using Economy.DataAccess.BlToolkit.AutomapperMappings;
using Economy.DataAccess.BlToolkit.DbManagers;
using Ninject.Modules;

namespace Economy.DataAccess.BlToolkit.IoC
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
            Bind<EntityDtoMappings>()
               .ToSelf()
               .InSingletonScope()
               .InstantlyCreate();

            Bind<IBaseSessionManager>()
                .To<EconomyDb>()
                .InTransientScope();

            Bind<IBaseSessionManagerFactory>()
                .To<DbManagerFactory>()
                .InTransientScope();
        }
    }
}