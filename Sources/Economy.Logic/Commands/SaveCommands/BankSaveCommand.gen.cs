//---------------------------------------------------------------------------------------------------
// auto-generated
//---------------------------------------------------------------------------------------------------
using System;
using CQRS.Logic.Commands;
using Economy.Dtos;

namespace Economy.Logic.Commands.SaveCommands
{
    public class BankSaveCommand : BaseCommand
    {
        public static readonly Guid Id = new Guid("5059E4BA-8311-4B9A-99FA-902804850B55");

        public override Guid CommandId => Id;

        public BankDto Dto { get; set; }

		public BankSaveCommand(BankDto dto)
        {
            Dto = dto;
        }
    }
}