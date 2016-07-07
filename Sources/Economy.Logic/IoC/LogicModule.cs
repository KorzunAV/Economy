using CQRS.Common;
using CQRS.Logic;
using CQRS.Logic.Blos;
using CQRS.Logic.Validation;
using Economy.DataAccess.BlToolkit.IoC;
using Economy.Logic.AutomapperMappings;
using Economy.Logic.Blos;
using Ninject;
using Ninject.Modules;
using Ninject.Parameters;

namespace Economy.Logic.IoC
{
    public class LogicModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DtoMappings>()
              .ToSelf()
              .InSingletonScope()
              .InstantlyCreate();


            Bind<BaseBlo>()
                .To<CurrencyTypeBlo>()
                .InSingletonScope();

            Bind<BaseBlo>()
                .To<BelinvestCourseArhiveBlo>()
                .InSingletonScope();

            Bind<BaseBlo>()
               .To<TransactionBlo>()
               .InSingletonScope();

            Bind<ICommandQueryDispatcher, ICommandQueryRegistrator>()
                .To<CommandQueryDispatcher>()
                .InSingletonScope()
                .WithConstructorArgument(new ConstructorArgument("blos", Kernel.GetAll<BaseBlo>()))
                .WithConstructorArgument(new ConstructorArgument("sessionManagerFactory", Kernel.Get<IBaseSessionManagerFactory>()));

            Bind<ValidationManager>()
                .To<ValidationManager>()
                .InSingletonScope();
            //TODO ? .WithConstructorArgument("validators", new Dictionary<Type, ValidatorBase>()); //new Dictionary<Type, ValidatorBase>()
        }
    }
}