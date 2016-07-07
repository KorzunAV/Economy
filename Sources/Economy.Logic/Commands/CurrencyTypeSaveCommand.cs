using System;
using CQRS.Logic.Commands;
using Economy.Dtos;

namespace Economy.Logic.Commands
{
    public class CurrencyTypeSaveCommand : BaseCommand
    {
        public static readonly Guid Id = new Guid("813FE653-1C30-46E0-9545-7C90851271D0");

        public override Guid CommandId
        {
            get { return Id; }
        }

        public CurrencyTypeDto Dto { get; set; }
    }
}