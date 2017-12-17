using System.Collections.Generic;
using Economy.Dtos;
using Economy.Logic.Commands.SaveCommands;
using Economy.Logic.Commands.SaveListCommands;
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

            var crez = CommandQueryDispatcher.ExecuteCommand<CurrencyTypeDto>(new CurrencyTypeSaveCommand(dto));
            Assert.IsTrue(crez.Data != null);
        }

        [Test]
        public void CurrencyTypeInsertListTest()
        {
            var dtos = new List<CurrencyTypeDto>
            {
                new CurrencyTypeDto
                {
                    Name = "Test",
                    ShortName = "TST"
                },
                new CurrencyTypeDto
                {
                    Name = "Test2",
                    ShortName = "TS2"
                },
                new CurrencyTypeDto
                {
                    Name = "Test",
                    ShortName = "TST"
                },
            };

            var crez = CommandQueryDispatcher.ExecuteCommand<List<CurrencyTypeDto>>(new CurrencyTypeSaveListCommand(dtos));
            Assert.IsTrue(crez.Data != null);
            Assert.IsTrue(crez.Data[0].Id > 0);
            Assert.IsTrue(crez.Data[1].Id > 0);
            Assert.IsTrue(crez.Data[2].Id > 0);
            Assert.IsTrue(crez.Data[1].Id > crez.Data[0].Id);
            Assert.IsTrue(crez.Data[0].Id == crez.Data[2].Id);
        }
    }
}