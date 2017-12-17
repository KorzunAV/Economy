//---------------------------------------------------------------------------------------------------
// auto-generated
//---------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using CQRS.Logic.Commands;
using Economy.Dtos;

namespace Economy.Logic.Commands.SaveListCommands
{
    public class WalletSaveListCommand : BaseCommand
    {
        public static readonly Guid Id = new Guid("2F95CB68-B147-460D-A8F1-E7F93E964911");

        public override Guid CommandId => Id;

        public List<WalletDto> Dtos { get; set; }

		public WalletSaveListCommand(List<WalletDto> dtos)
        {
            Dtos = dtos;
        }
    }
}