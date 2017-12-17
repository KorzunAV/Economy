//---------------------------------------------------------------------------------------------------
// auto-generated
//---------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using CQRS.Logic.Commands;
using Economy.Dtos;

namespace Economy.Logic.Commands.SaveListCommands
{
    public class MontlyReportSaveListCommand : BaseCommand
    {
        public static readonly Guid Id = new Guid("402234F9-E485-4604-A2A5-3A61E8B95BF1");

        public override Guid CommandId => Id;

        public List<MontlyReportDto> Dtos { get; set; }

		public MontlyReportSaveListCommand(List<MontlyReportDto> dtos)
        {
            Dtos = dtos;
        }
    }
}