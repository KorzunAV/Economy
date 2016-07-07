using System;
using System.Collections.Generic;
using CQRS.Logic.Commands;
using Economy.Dtos;

namespace Economy.Logic.Commands
{
    public class MontlyReportSaveAllCommand : BaseCommand
    {
        public static readonly Guid Id = new Guid("19F013AE-EE10-4F1A-ADE2-6E22B4CC85EF");

        public override Guid CommandId
        {
            get { return Id; }
        }

        public MontlyReportSaveAllCommand(List<MontlyReportDto> dtos)
        {
            Dtos = dtos;
        }

        public List<MontlyReportDto> Dtos { get; set; }
    }
}