using AutoMapper;
using CQRS.Logic;
using Economy.DataAccess.BlToolkit.AutomapperMappings;
using Economy.DataAccess.BlToolkit.IoC;
using Economy.Logic.AutomapperMappings;
using Economy.Logic.IoC;
using Ninject;
using NUnit.Framework;

namespace Economy.Test.LogicTests
{
    public class TestFixtureBase
    {
        protected ICommandQueryDispatcher CommandQueryDispatcher;
        protected IKernel Kernel;

        [OneTimeSetUp]
        public virtual void OneTimeSetUp()
        {
            Mapper.Initialize(cfg =>
            {
                EntityDtoMappings.Initialize(cfg);
                DtoMappings.Initialize(cfg);
            });

            Kernel = new StandardKernel(new DataAccessModule(), new LogicTestModule());
            CommandQueryDispatcher = Kernel.Get<ICommandQueryDispatcher>();
        }
    }
}