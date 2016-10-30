using System;
using System.Collections.Generic;
using Economy.Dtos;
using Economy.Logic.Commands;
using NUnit.Framework;

namespace Economy.Test.LogicTests
{
    [TestFixture]
    public class CourseArhive : TestFixtureBase
    {
        [Test]
        public void CourseArhiveSaveCommandTest()
        {
            var cur = new CurrencyTypeDto
            {
                Name = "Test",
                ShortName = "TST"
            };

            var minDto = new CourseArhiveDto
            {
                Buy = decimal.MinValue,
                Sel = decimal.MinValue,
                RegDate = DateTime.MinValue,
                CurrencyType = cur
            };
            var command = new CourseArhiveSaveCommand { Dto = minDto };
            CommandQueryDispatcher.ExecuteCommand<bool>(command);

            var maxDto = new CourseArhiveDto
            {
                Buy = decimal.MaxValue,
                Sel = decimal.MaxValue,
                RegDate = DateTime.MaxValue,
                CurrencyType = cur
            };
            command = new CourseArhiveSaveCommand { Dto = maxDto };
            CommandQueryDispatcher.ExecuteCommand<bool>(command);
        }
    }
}