//---------------------------------------------------------------------------------------------------
// auto-generated
//---------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using CQRS.Logic.Commands;
using Economy.Dtos;

namespace Economy.Logic.Commands.SaveListCommands
{
    public class TransactionSaveListCommand : BaseCommand
    {
        public static readonly Guid Id = new Guid("2576CF59-B772-43E3-B2B6-67B057779B57");

        public override Guid CommandId => Id;

        public List<TransactionDto> Dtos { get; set; }

		public TransactionSaveListCommand(List<TransactionDto> dtos)
        {
            Dtos = dtos;
        }
    }
}