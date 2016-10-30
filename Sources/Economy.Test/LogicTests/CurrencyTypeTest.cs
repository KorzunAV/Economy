using Economy.Dtos;
using Economy.Logic.Commands;
using NUnit.Framework;

namespace Economy.Test.LogicTests
{
    [TestFixture]
    public class CurrencyTypeTest : TestFixtureBase
    {
        [Test]
        public void CurrencyTypeInsertTest()
        {
            var dto = new CurrencyTypeDto
            {
                Name = "Test",
                ShortName = "TST"
            };

            var command = new CurrencyTypeSaveCommand
            {
                Dto = dto
            };
            var crez = CommandQueryDispatcher.ExecuteCommand<int>(command);
            Assert.IsTrue(crez.Data > 0);
        }
    }
}