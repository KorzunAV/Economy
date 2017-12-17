//---------------------------------------------------------------------------------------------------
// auto-generated
//---------------------------------------------------------------------------------------------------
using System;
using CQRS.Logic.Commands;
using Economy.Dtos;

namespace Economy.Logic.Commands.SaveCommands
{
    public class MontlyReportSaveCommand : BaseCommand
    {
        public static readonly Guid Id = new Guid("DFF4BAE9-0356-44FC-9D6B-4BFE73D7FF38");

        public override Guid CommandId => Id;

        public MontlyReportDto Dto { get; set; }

		public MontlyReportSaveCommand(MontlyReportDto dto)
        {
            Dto = dto;
        }
    }
}