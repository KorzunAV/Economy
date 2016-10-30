using System;
using CQRS.Logic.Commands;
using Economy.Dtos;

namespace Economy.Logic.Commands
{
    public class MontlyReportSaveCommand : BaseCommand
    {
        public static readonly Guid Id = new Guid("19F013AE-EE10-4F1A-ADE2-6E22B4CC85EF");

        public override Guid CommandId
        {
            get { return Id; }
        }

        public MontlyReportSaveCommand(MontlyReportDto dto)
        {
            Dto = dto;
        }

        public MontlyReportDto Dto { get; set; }
    }
}