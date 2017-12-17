//---------------------------------------------------------------------------------------------------
// auto-generated
//---------------------------------------------------------------------------------------------------
using System;
using CQRS.Logic.Commands;
using Economy.Dtos;

namespace Economy.Logic.Commands.SaveCommands
{
    public class WalletSaveCommand : BaseCommand
    {
        public static readonly Guid Id = new Guid("63403B17-0DE4-418B-B4BD-4B24945518D0");

        public override Guid CommandId => Id;

        public WalletDto Dto { get; set; }

		public WalletSaveCommand(WalletDto dto)
        {
            Dto = dto;
        }
    }
}