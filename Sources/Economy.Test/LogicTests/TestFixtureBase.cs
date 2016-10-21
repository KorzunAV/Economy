using CQRS.Logic;
using Economy.DataAccess.BlToolkit.IoC;
using Economy.Logic.IoC;
using Ninject;
using NUnit.Framework;

namespace Economy.Test.LogicTests
{
    public class TestFixtureBase
    {
        protected ICommandQueryDispatcher CommandQueryDispatcher;
        protected IKernel Kernel;
        
        [TestFixtureSetUp]
        public virtual void TestFixtureSetUp()
        {
            Kernel = new StandardKernel(new DataAccessModule(), new LogicTestModule());
            CommandQueryDispatcher = Kernel.Get<ICommandQueryDispatcher>();
        }
    }
}