﻿using CQRS.Common;
using Economy.DataAccess.BlToolkit.AutomapperMappings;
using Economy.DataAccess.BlToolkit.Daos;
using Economy.DataAccess.BlToolkit.DbManagers;
using Ninject.Modules;

namespace Economy.DataAccess.BlToolkit.IoC
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

        private void BindDaos()
        {
            Bind<BaseDao>()
                .ToSelf()
                .InSingletonScope();

            Bind<BelinvestCourseArhiveDao>()
                .ToSelf()
                .InSingletonScope();

            Bind<CurrencyTypeDao>()
                .ToSelf()
                .InSingletonScope();
        }
    }
}