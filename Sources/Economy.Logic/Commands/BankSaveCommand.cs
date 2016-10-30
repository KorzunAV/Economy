using System;
using CQRS.Logic.Commands;
using Economy.Dtos;

namespace Economy.Logic.Commands
{
    public class BankSaveCommand : BaseCommand
    {
        public static readonly Guid Id = new Guid("331D5B9E-CF20-48C4-9215-16C86BF56C5E");

        public override Guid CommandId
        {
            get { return Id; }
        }

        public BankDto Dtos { get; set; }
    }
}