//---------------------------------------------------------------------------------------------------
// auto-generated
//---------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using CQRS.Logic.Commands;
using Economy.Dtos;

namespace Economy.Logic.Commands.SaveListCommands
{
    public class CurrencyTypeSaveListCommand : BaseCommand
    {
        public static readonly Guid Id = new Guid("63FC3DE7-381B-4C4E-80FC-13442770B0C4");

        public override Guid CommandId => Id;

        public List<CurrencyTypeDto> Dtos { get; set; }

		public CurrencyTypeSaveListCommand(List<CurrencyTypeDto> dtos)
        {
            Dtos = dtos;
        }
    }
}