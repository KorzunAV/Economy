using System;
using Economy.Dtos;
using Economy.Logic.Commands;
using Economy.Logic.Commands.SaveCommands;
using NUnit.Framework;

namespace Economy.Test.LogicTests
{
    [TestFixture]
    public class CourseArhive : TestFixtureBase
    {
        [Test]
        public void CourseArhiveSaveCommandsuccesTest()
        {
            var cur = new CurrencyTypeDto
            {
                Name = "Test",
                ShortName = "TST"
            };

            var bank = new BankDto
            {
                Name = "TestBank"
            };

            var dto = new CourseArhiveDto
            {
                Buy = 10,
                Sel = 9,
                RegDate = DateTime.Today,
                CurrencyType = cur,
                Bank = bank
            };
            var result = CommandQueryDispatcher.ExecuteCommand<CourseArhiveDto>(new CourseArhiveSaveCommand(dto));
            Assert.IsTrue(result.Data != null);

        }

        [Test]
        public void CourseArhiveSaveCommandErrorMinTest()
        {
            var cur = new CurrencyTypeDto
            {
                Name = "Test",
                ShortName = "TST"
            };

            var bank = new BankDto
            {
                Name = "TestBank"
            };

            var minDto = new CourseArhiveDto
            {
                Buy = decimal.MinValue,
                Sel = decimal.MinValue,
                RegDate = DateTime.MinValue,
                CurrencyType = cur,
                Bank = bank
            };
            CommandQueryDispatcher.ExecuteCommand<CourseArhiveDto>(new CourseArhiveSaveCommand(minDto));
        }

        [Test]
        public void CourseArhiveSaveCommandErrorMaxTest()
        {
            var cur = new CurrencyTypeDto
            {
                Name = "Test",
                ShortName = "TST"
            };

            var bank = new BankDto
            {
                Name = "TestBank"
            };
            
            var maxDto = new CourseArhiveDto
            {
                Buy = decimal.MaxValue,
                Sel = decimal.MaxValue,
                RegDate = DateTime.MaxValue,
                CurrencyType = cur,
                Bank = bank
            };
            CommandQueryDispatcher.ExecuteCommand<CourseArhiveDto>(new CourseArhiveSaveCommand(maxDto));
        }
    }
}