using System;
using System.Collections.Generic;
using Economy.Dtos;
using Economy.Logic.Commands;
using NUnit.Framework;

namespace Economy.Test.LogicTests
{
    [TestFixture]
    class CourseArhive : TestFixtureBase
    {
        //[Test]
        public void CourseArhiveSaveCommandTest()
        {
            var cur = new CurrencyTypeDto
            {
                Name = "Test",
                ShortName = "TST"
            };


            var minDto = new CourseArhiveDto();
            minDto.Buy = decimal.MinValue;
            minDto.Sel = decimal.MinValue;
            minDto.RegDate = DateTime.MinValue;
            minDto.CurrencyTypeDto = cur;

            var maxDto = new CourseArhiveDto();
            minDto.Buy = decimal.MaxValue;
            minDto.Sel = decimal.MaxValue;
            minDto.RegDate = DateTime.MaxValue;
            minDto.CurrencyTypeDto = cur;

            var set = new List<CourseArhiveDto> { minDto, maxDto };



            var command = new CourseArhiveSaveCommand
            {
                Dtos = set
            };
            CommandQueryDispatcher.ExecuteCommand<bool>(command);
        }
    }
}