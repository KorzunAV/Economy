//---------------------------------------------------------------------------------------------------
// auto-generated
//---------------------------------------------------------------------------------------------------
using System;
using CQRS.Logic.Commands;
using Economy.Dtos;

namespace Economy.Logic.Commands.SaveCommands
{
    public class TransactionSaveCommand : BaseCommand
    {
        public static readonly Guid Id = new Guid("8D0F37DF-1494-415E-97A3-6ACA4A82CD29");

        public override Guid CommandId => Id;

        public TransactionDto Dto { get; set; }

		public TransactionSaveCommand(TransactionDto dto)
        {
            Dto = dto;
        }
    }
}