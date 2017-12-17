//---------------------------------------------------------------------------------------------------
// auto-generated
//---------------------------------------------------------------------------------------------------
using System;
using CQRS.Logic.Commands;
using Economy.Dtos;

namespace Economy.Logic.Commands.SaveCommands
{
    public class CurrencyTypeSaveCommand : BaseCommand
    {
        public static readonly Guid Id = new Guid("B7909AF3-BA92-4F1E-A778-AC173D8D6736");

        public override Guid CommandId => Id;

        public CurrencyTypeDto Dto { get; set; }

		public CurrencyTypeSaveCommand(CurrencyTypeDto dto)
        {
            Dto = dto;
        }
    }
}