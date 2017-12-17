//---------------------------------------------------------------------------------------------------
// auto-generated
//---------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using CQRS.Logic.Commands;
using Economy.Dtos;

namespace Economy.Logic.Commands.SaveListCommands
{
    public class BankSaveListCommand : BaseCommand
    {
        public static readonly Guid Id = new Guid("46E88E33-2F39-4100-BE98-2C912C990FC0");

        public override Guid CommandId => Id;

        public List<BankDto> Dtos { get; set; }

		public BankSaveListCommand(List<BankDto> dtos)
        {
            Dtos = dtos;
        }
    }
}